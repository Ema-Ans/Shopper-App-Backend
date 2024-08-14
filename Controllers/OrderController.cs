using Microsoft.AspNetCore.Mvc;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;
using Shopper_Demo.DTOs;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Shopper_Demo.Controllers
{
    [ApiController]
    [Route("orders")]

    public class OrderController : ControllerBase
    {
        private readonly IInMemoryRepository _repository;
        private readonly IInMemoryOrder _orderlist;
        private readonly IInMemoryShopper _shopperlist;

        
        public OrderController(IInMemoryRepository repository, IInMemoryOrder orderlist, IInMemoryShopper shopperlist)
        {
            _repository = repository;
            _orderlist = orderlist;
            _shopperlist = shopperlist;

        }

          // GET / orders
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = _orderlist.GetOrders().Select(order => order.AsOrderDto());
            return Ok(orders);
        }


        // GET /orders/{orderId}
        [HttpGet("{orderId}")]
        public ActionResult<OrderDto> GetOrderById(Guid orderId)
        {
            var order = _orderlist.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.AsOrderDto());
        }


        // GET orders by shopperid
        [HttpGet("shopper/{shopperId}")]
        public ActionResult<IEnumerable<OrderDto>> GetOrdersByShopperId(Guid shopperId)
        {
            var orders = _orderlist.GetOrders().Where(o => o.Shopper_Id == shopperId).Select(order => order.AsOrderDto());
            if (!orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        //  When an order is created, the orderitem repository would be used to add items
        // POST / orders
        // TODO: make sure to check with the inventory and update the inventory
        [HttpPost]
        public ActionResult<OrderDto> CreateOrder(CreateOrderDto orderDto)
        {
            decimal totalPrice = orderDto.OrderItemsList.Sum(item => item.Price * item.Quantity);
            int totalItems = orderDto.OrderItemsList.Sum(item => item.Quantity);
            int totalProducts = orderDto.OrderItemsList.Count;

            Order newOrder = new()
            {
                OrderId = Guid.NewGuid(),
                ShopperName = null,
                Shopper_Id = null,
                TotalPrice = totalPrice,
                TotalItems = totalItems,
                TotalProducts = totalProducts,
                CreatedDate = DateTimeOffset.UtcNow,
                InProgress = false,
                OrderItemsList = orderDto.OrderItemsList
            };

            _orderlist.CreateOrder(newOrder);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder.AsOrderDto());


        }


        // PUT /orders/{id}
        [HttpPut("{OrderId}")]
        public ActionResult UpdateOrder(Guid OrderId, UpdateOrderDto orderDto)
        {
            var existingOrder = _orderlist.GetOrderById(OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            Order updatedOrder = existingOrder with
            {
                ShopperName = orderDto.ShopperName,
                Shopper_Id = orderDto.Shopper_Id,
                TotalPrice = orderDto.TotalPrice,
                TotalItems = orderDto.TotalItems,
                TotalProducts = orderDto.TotalProducts,
                InProgress = orderDto.InProgress,
                OrderItemsList = orderDto.OrderItemsList
            };

          

            _orderlist.UpdateOrder(updatedOrder);

            return NoContent();
        }
          
          // DELETE /orders/{id}
        [HttpDelete("{OrderId}")]
        public ActionResult DeleteOrder(Guid OrderId)
        {
            var existingOrder = _orderlist.GetOrderById(OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderlist.DeleteOrder(OrderId);

            return NoContent();
        }

         // POST /orders/{id}/assign
        [HttpPost("{OrderId}/assign")]
        public ActionResult AssignOrder(Guid OrderId, Guid ShopperId)
        {
            var existingOrder = _orderlist.GetOrderById(OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            var shopper = _shopperlist.GetShopperById(ShopperId);
            if (shopper == null)
            {
                return BadRequest("Shopper not found.");
            }
            Order assignedOrder = existingOrder with
            {
                    
                    Shopper_Id = shopper.ShopperId,
                    ShopperName = shopper.ShopperName,
                    InProgress = true
            };
            _orderlist.UpdateOrder(assignedOrder);

            // Add the order to the shopper's OrdersAssigned list
            shopper.OrdersAssigned.Add(assignedOrder);
            _shopperlist.UpdateShopper(shopper);


            return NoContent();
        }

         // POST /orders/{id}/complete
        [HttpPost("{OrderId}/complete")]
        public ActionResult CompleteOrder(Guid OrderId)
        {
            var existingOrder = _orderlist.GetOrderById(OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            var shopper = _shopperlist.GetShopperById(existingOrder.Shopper_Id.Value);
            if (shopper == null)
            {
                return BadRequest("Shopper not found.");
            }

            // Remove the order from the shopper's OrdersAssigned list
            shopper.OrdersAssigned.Remove(existingOrder);
            // Add the order to the shopper's PastOrders list
            shopper.PastOrders.Add(existingOrder);



            existingOrder.InProgress = false;

            _orderlist.UpdateOrder(existingOrder);
            _shopperlist.UpdateShopper(shopper);

            return NoContent();
        }



        // update order

        // deleteorder

        // assign order

        // complete order



 
    }
}