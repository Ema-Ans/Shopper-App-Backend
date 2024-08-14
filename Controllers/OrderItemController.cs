using Microsoft.AspNetCore.Mvc;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;
using Shopper_Demo.DTOs;
using System.Collections.Generic;
using System;
using System.Linq;
// Each orderitem will correspond to a product
namespace Shopper_Demo.Controllers
{
    [ApiController]
    [Route("orderitems")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IInMemoryRepository _repository;
        private readonly IInMemoryInventory _inventory;


        public OrderItemsController(IInMemoryRepository repository, IInMemoryInventory inventory)
        {
            _repository = repository;
            _inventory = inventory;

        }

        // GET / orderitems
        [HttpGet]
        public IEnumerable<OrderItemDto> GetOrderItems()
        {
            var items = _repository.GetOrderItems().Select(item => item.AsDto());
            return items;
        }

        // GET / orderitems/{id}
        [HttpGet("{id}")]
        public ActionResult<OrderItemDto> GetOrderItem(Guid id)
        {
            var orderItem = _repository.GetOrderItem(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem.AsDto());
        }

        //  When an order is created, the inventory would be checked
        // POST / orderitems
        // TODO: make sure to check with the inventory and update the inventory
        [HttpPost]
        public ActionResult<OrderItemDto> CreateOrderItem(CreateOrderItemDto itemDto)
        {
            var product = _inventory.GetProduct(itemDto.ProductId);
            if (product == null)
            {
                return BadRequest("Product not found.");
            }

            if (product.QuantityAvailable < itemDto.Quantity)
            {
                return BadRequest("Insufficient quantity in inventory.");
            }
            
            OrderItem item = new()
            {
                ItemId = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                Quantity = itemDto.Quantity,
                CreatedDate = DateTimeOffset.UtcNow,
                // TODO: change this to ask the inventory
                InStock = true,
                ProductId =  itemDto.ProductId
            };

            _repository.CreateOrderItem(item);
            //TODO: ask Nayef if it makes sense to decrease quantity when adding item
            _inventory.DecreaseQuantity(itemDto.ProductId, itemDto.Quantity);

            return CreatedAtAction(nameof(GetOrderItem), new { id = item.ItemId }, item.AsDto());
        }

        // PUT/ orderitems / {id}
        // TODO: update the invenotry too
        // PUT/ orderitems / {id}
        [HttpPut("{id}")]
        public ActionResult UpdateOrderItem(Guid id, UpdateOrderItemDto itemDto)
        {
            var existingItem = _repository.GetOrderItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            var product = _inventory.GetProduct(itemDto.ProductId);
            if (product == null)
            {
                return BadRequest("Product not found.");
            }

            if (product.QuantityAvailable + existingItem.Quantity < itemDto.Quantity)
            {
                return BadRequest("Insufficient quantity in inventory.");
            }

            _inventory.IncreaseQuantity(existingItem.ProductId, existingItem.Quantity);
            _inventory.DecreaseQuantity(itemDto.ProductId, itemDto.Quantity);

            OrderItem updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Quantity = itemDto.Quantity,
                ProductId = itemDto.ProductId
            };

            _repository.UpdateOrderItem(updatedItem);

            return NoContent();
        }

        //DELETE / orderitems/ {id}
        [HttpDelete("{id}")]

        public ActionResult DeleteOrderItem(Guid id)
        {
            // Before removing, tring checking if it exists
            var existingItem = _repository.GetOrderItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            _repository.DeleteOrderItem(id);
            _inventory.IncreaseQuantity(existingItem.ProductId, existingItem.Quantity);

            return NoContent();
        }
    }
}
