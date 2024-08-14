using Microsoft.AspNetCore.Mvc;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;
using Shopper_Demo.DTOs;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel;


namespace Shopper_Demo.Controllers
{
    [ApiController]
    [Route("inventoryproducts")]
    public class InventoryProdContoller : ControllerBase
    {
        private readonly IInMemoryInventory _inventory;

        public InventoryProdContoller(IInMemoryInventory inventory)
        {
            _inventory = inventory;
        }

        // GET / inventoryproducts/ 
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            var product = _inventory.GetProducts().Select(prod => prod.AsProdDto());
            return product;
        }

        // GET / inventoryproducts/ {id}
        [HttpGet("id")]
        public ActionResult<Product?> GetProduct(Guid id)
        {
            var product = _inventory.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.AsProdDto());
        }

        // POST / inventoryproducts
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto prodDto)
        {
            Product prod = new()
            {
                ProdId = Guid.NewGuid(),
                Name = prodDto.Name,
                Description = prodDto.Description,
                Category = prodDto.Category,
                Brand = prodDto.Brand,
                QuantityAvailable = prodDto.QuantityAvailable,
                CostPrice = prodDto.CostPrice,
                SellingPrice = prodDto.SellingPrice,
                DateAdded =  DateTimeOffset.UtcNow,
                ExpirationDate = prodDto.ExpirationDate

            };

            _inventory.CreateProduct(prod);
            return CreatedAtAction(nameof(GetProduct), new {id = prod.ProdId}, prod.AsProdDto());
        }

        // PUT/ inventoryproduct / {id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, UpdateProductDto prodDto)
        {
            // First try to find the item if it exists otherwise we return Notfound
            var existingProd = _inventory.GetProduct(id);
            if (existingProd is null)
            {
                return NotFound();
            }

            Product updatedProd =  existingProd with
            {
                Name = prodDto.Name,
                Description = prodDto.Description,
                Category = prodDto.Category,
                Brand = prodDto.Brand,
                QuantityAvailable = prodDto.QuantityAvailable,
                CostPrice = prodDto.CostPrice,
                SellingPrice = prodDto.SellingPrice,
                ExpirationDate = prodDto.ExpirationDate

            };

            _inventory.UpdateProduct(updatedProd);

            return NoContent();
        
        }

        // DELETE / inventoryproduct / {id}
        [HttpDelete("{id}")]

        public ActionResult DeleteProduct(Guid id)
        {
            var existingProd = _inventory.GetProduct(id);
            if (existingProd is null)
            {
                return NotFound();
            }

            _inventory.DeleteProduct(id);
            
            return NoContent();
        }
        
        // PATCH /inventoryproducts/decrease/{id}

        [HttpPatch("decrease/{id}")]

        public ActionResult DecreaseQuantity(Guid id, int quantity)
        {
            var existingProd = _inventory.GetProduct(id);
            if (existingProd is null)
            {
                return NotFound();
            }

            _inventory.DecreaseQuantity(id, quantity);

            return NoContent();

        }

        // PATCH /inventoryproducts/increase/{id}
        [HttpPatch("increase/{id}")]
        public ActionResult IncreaseQuantity(Guid id, int quantity)
        {
            var existingProd = _inventory.GetProduct(id);
            if (existingProd is null)
            {
                return NotFound();
            }

            _inventory.IncreaseQuantity(id, quantity);

            return NoContent();
        }

    }
}