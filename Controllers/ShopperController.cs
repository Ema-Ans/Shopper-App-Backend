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
    [Route("shoppers")]

    public class ShopperController : ControllerBase
    {
        private readonly IInMemoryShopper _shopper;

        public ShopperController(IInMemoryShopper shopper)
        {
            _shopper = shopper;

        }

        // GET / shoppers
        [HttpGet]
        public IEnumerable<ShopperDto> GetShoppers()
        {
            var shoppers = _shopper.GetShoppers().Select(shop => shop.AsShopperDto());
            return shoppers;
        }

        // GET / shoppers / {id}
        [HttpGet("{ShopperId}")]
        public ActionResult<ShopperDto> GetShopperById(Guid ShopperId)
        {
            var shopper = _shopper.GetShopperById(ShopperId);
            if (shopper == null)
            {
                return NotFound();
            }
            return Ok(shopper.AsShopperDto());
        }

        // POST /shoppers
        [HttpPost]
        public ActionResult<ShopperDto> CreateShopper(CreateShopperDto shopperDto)
        {
            Shopper newShopper = new()
            {
                ShopperId = Guid.NewGuid(),
                ShopperName = shopperDto.ShopperName,
                UserName = shopperDto.UserName,
                PasswordHash = shopperDto.PasswordHash,
                Email = shopperDto.Email,
                PhoneNumber = shopperDto.PhoneNumber,
                // Address = shopperDto.Address,
                CreatedDate = DateTimeOffset.UtcNow,
                OrdersAssigned = new List<Order>(),
                PastOrders = new List<Order>()
            };

            _shopper.CreateShopper(newShopper);
            return CreatedAtAction(nameof(GetShopperById), new { id = newShopper.ShopperId }, newShopper.AsShopperDto());
        }

         // PUT /shoppers/{id}
        [HttpPut("{ShopperId}")]
        public ActionResult UpdateShopper(Guid ShopperId, UpdateShopperDto shopperDto)
        {
            var existingShopper = _shopper.GetShopperById(ShopperId);
            if (existingShopper == null)
            {
                return NotFound();
            }

            Shopper updatedShopper = existingShopper with
            {
                ShopperName = shopperDto.ShopperName,
                UserName = shopperDto.UserName,
                PasswordHash = shopperDto.PasswordHash,
                Email = shopperDto.Email,
                PhoneNumber = shopperDto.PhoneNumber
            };

            _shopper.UpdateShopper(updatedShopper);

            return NoContent();
        }


        // DELETE /shoppers/{ShopperId}
        [HttpDelete("{ShopperId}")]
        public ActionResult DeleteShopper(Guid ShopperId)
        {
            var existingShopper = _shopper.GetShopperById(ShopperId);
            if (existingShopper == null)
            {
                return NotFound();
            }

            _shopper.DeleteShopper(ShopperId);

            return NoContent();
        }

        // GET /shoppers/{id}/completedorders
        [HttpGet("{ShopperId}/completedorders")]
        public ActionResult<IEnumerable<OrderDto>> GetCompletedOrders(Guid ShopperId)
        {
            var orders = _shopper.GetOrdersCompleted(ShopperId);
            if (orders == null)
            {
                return NotFound();
            }
            var orderDtos = orders.Select(order => order.AsOrderDto());
            return Ok(orderDtos);
        }

        // GET /shoppers/{id}/pastorders
        [HttpGet("{ShopperId}/pastorders")]
        public ActionResult<IEnumerable<OrderDto>> GetPastOrders(Guid ShopperId)
        {
            var orders = _shopper.GetPastOrders(ShopperId);
            if (orders == null)
            {
                return NotFound();
            }
            var orderDtos = orders.Select(order => order.AsOrderDto());
            return Ok(orderDtos);
        }




    }
}