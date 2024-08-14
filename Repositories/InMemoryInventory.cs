using System;
using System.Collections.Generic;
using System.Linq;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;

namespace Shopper_Demo.Repositories
{
    public class InMemoryInventory : IInMemoryInventory
    {
        private readonly List<Product> inventoryproducts = new()
        {new Product
                {
                    ProdId = Guid.NewGuid(),
                    Name = "Shoes",
                    Description = "Things to cover feet",
                    Category = "Footwear",
                    Brand = "Hush Puppies",
                    QuantityAvailable = 10,
                    CostPrice = 100.0m,
                    SellingPrice = 250.0m,
                    DateAdded = DateTimeOffset.UtcNow,
                    ExpirationDate = null
                },
        new Product
                {
                    ProdId = Guid.NewGuid(),
                    Name = "Bags",
                    Description = "Things to carry items",
                    Category = "Accessories",
                    Brand = "Hush Puppies",
                    QuantityAvailable = 100,
                    CostPrice = 100.0m,
                    SellingPrice = 250.0m,
                    DateAdded = DateTimeOffset.UtcNow,
                    ExpirationDate = null
                },
        new Product
                {
                    ProdId = Guid.NewGuid(),
                    Name = "Shirts",
                    Description = "Things to cover body",
                    Category = "Clothes",
                    Brand = "H&M",
                    QuantityAvailable = 10,
                    CostPrice = 100.0m,
                    SellingPrice = 250.0m,
                    DateAdded = DateTimeOffset.UtcNow,
                    ExpirationDate = null
                },
        new Product
                {
                    ProdId = Guid.NewGuid(),
                    Name = "Makeup",
                    Description = "Things to enhance appearance",
                    Category = "Accessories",
                    Brand = "Hush Puppies",
                    QuantityAvailable = 10,
                    CostPrice = 100.0m,
                    SellingPrice = 250.0m,
                    DateAdded = DateTimeOffset.UtcNow,
                    ExpirationDate = null
                }
        };

        public Dictionary<string, Guid> ProductIdMap { get; }

        public InMemoryInventory()
        {
            ProductIdMap = inventoryproducts.ToDictionary(p => p.Name, p => p.ProdId);
        }

        public Product? GetProduct(Guid id) => inventoryproducts.SingleOrDefault(item => item.ProdId == id);

        public IEnumerable<Product> GetProducts() => inventoryproducts;

        public void CreateProduct(Product prod)
        {
            inventoryproducts.Add(prod);
            ProductIdMap[prod.Name] = prod.ProdId;
        }

        public void UpdateProduct(Product prod)
        {
            var index = inventoryproducts.FindIndex(existingProd => existingProd.ProdId == prod.ProdId);
            if (index != -1) inventoryproducts[index] = prod;
        }

        public void DeleteProduct(Guid id)
        {
            var index = inventoryproducts.FindIndex(existingProd => existingProd.ProdId == id);
            if (index != -1)
            {
                var prod = inventoryproducts[index];
                inventoryproducts.RemoveAt(index);
                ProductIdMap.Remove(prod.Name);
            }
        }

        public void DecreaseQuantity(Guid id, int quantity)
        {
            var product = inventoryproducts.SingleOrDefault(p => p.ProdId == id);
            if (product != null && product.QuantityAvailable >= quantity)
                product.QuantityAvailable -= quantity;
            else
                throw new InvalidOperationException("Product not found or quantity is too low");
        }

        public void IncreaseQuantity(Guid id, int quantity)
        {
            var product = inventoryproducts.SingleOrDefault(p => p.ProdId == id);
            if (product != null)
                product.QuantityAvailable += quantity;
            else
                throw new InvalidOperationException("Product not found");
        }
    }
}
 

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Shopper_Demo.Entities;
// using Shopper_Demo.Interfaces;

// namespace Shopper_Demo.Repositories
// {
//     public class InMemoryInventory : IInMemoryInventory
//     {
//         private readonly List<Product> inventoryproducts;
//         private readonly Dictionary<string, Guid> ProductIdMap;
//         private readonly List<OrderItem> orderitems;


//         public InMemoryInventory()
//         {
//             inventoryproducts = new List<Product>
//             {
//                 new Product
//                 {
//                     ProdId = Guid.NewGuid(),
//                     Name = "Shoes",
//                     Description = "Things to cover feet",
//                     Category = "Footwear",
//                     Brand = "Hush Puppies",
//                     QuantityAvailable = 10,
//                     CostPrice = 100.0m,
//                     SellingPrice = 250.0m,
//                     DateAdded = DateTimeOffset.UtcNow,
//                     ExpirationDate = null
//                 },
//                 new Product
//                 {
//                     ProdId = Guid.NewGuid(),
//                     Name = "Bags",
//                     Description = "Things to carry items",
//                     Category = "Accessories",
//                     Brand = "Hush Puppies",
//                     QuantityAvailable = 100,
//                     CostPrice = 100.0m,
//                     SellingPrice = 250.0m,
//                     DateAdded = DateTimeOffset.UtcNow,
//                     ExpirationDate = null
//                 },
//                 new Product
//                 {
//                     ProdId = Guid.NewGuid(),
//                     Name = "Shirts",
//                     Description = "Things to cover body",
//                     Category = "Clothes",
//                     Brand = "H&M",
//                     QuantityAvailable = 10,
//                     CostPrice = 100.0m,
//                     SellingPrice = 250.0m,
//                     DateAdded = DateTimeOffset.UtcNow,
//                     ExpirationDate = null
//                 },
//                 new Product
//                 {
//                     ProdId = Guid.NewGuid(),
//                     Name = "Makeup",
//                     Description = "Things to enhance appearance",
//                     Category = "Accessories",
//                     Brand = "Hush Puppies",
//                     QuantityAvailable = 10,
//                     CostPrice = 100.0m,
//                     SellingPrice = 250.0m,
//                     DateAdded = DateTimeOffset.UtcNow,
//                     ExpirationDate = null
//                 }
//             };

//             // Create a dictionary to map product names to their IDs
//             productIdMap = inventoryproducts.ToDictionary(p => p.Name, p => p.ProdId);

//             orderitems = new List<OrderItem>
//             {
//                 new OrderItem
//                 {
//                     ItemId = Guid.NewGuid(),
//                     Name = "Shoes",
//                     Price = 9,
//                     CreatedDate = DateTimeOffset.UtcNow,
//                     Quantity = 10,
//                     InStock = true,
//                     ProductId = productIdMap["Shoes"]
//                 },
//                 new OrderItem
//                 {
//                     ItemId = Guid.NewGuid(),
//                     Name = "Socks",
//                     Price = 5,
//                     CreatedDate = DateTimeOffset.UtcNow,
//                     Quantity = 10,
//                     InStock = true,
//                     // Assuming you have a product for socks, otherwise, create one in the inventory
//                     ProductId = productIdMap.ContainsKey("Socks") ? productIdMap["Socks"] : Guid.NewGuid()
//                 },
//                 new OrderItem
//                 {
//                     ItemId = Guid.NewGuid(),
//                     Name = "Glasses",
//                     Price = 7,
//                     CreatedDate = DateTimeOffset.UtcNow,
//                     Quantity = 10,
//                     InStock = true,
//                     // Assuming you have a product for glasses, otherwise, create one in the inventory
//                     ProductId = productIdMap.ContainsKey("Glasses") ? productIdMap["Glasses"] : Guid.NewGuid()
//                 }
//             };
//         }

//         public Product? GetProduct(Guid id)
//         {
//             return inventoryproducts.SingleOrDefault(item => item.ProdId == id);
//         }

//         public IEnumerable<Product> GetProducts()
//         {
//             return inventoryproducts;
//         }

//         public void CreateProduct(Product prod)
//         {
//             inventoryproducts.Add(prod);
//         }

//         public void UpdateProduct(Product prod)
//         {
//             var index = inventoryproducts.FindIndex(existingProd => existingProd.ProdId == prod.ProdId);
//             if (index != -1)
//             {
//                 inventoryproducts[index] = prod;
//             }
//         }

//         public void DeleteProduct(Guid id)
//         {
//             var index = inventoryproducts.FindIndex(existingProd => existingProd.ProdId == id);
//             if (index != -1)
//             {
//                 inventoryproducts.RemoveAt(index);
//             }
//         }

//         public void DecreaseQuantity(Guid id, int quantity)
//         {
//             var product = inventoryproducts.SingleOrDefault(p => p.ProdId == id);
//             if (product != null && product.QuantityAvailable >= quantity)
//             {
//                 product.QuantityAvailable -= quantity;
//             }
//             else
//             {
//                 throw new InvalidOperationException("Product not found or quantity is too less");
//             }
//         }

//         public void IncreaseQuantity(Guid id, int quantity)
//         {
//             var product = inventoryproducts.SingleOrDefault(p => p.ProdId == id);
//             if (product != null)
//             {
//                 product.QuantityAvailable += quantity;
//             }
//             else
//             {
//                 throw new InvalidOperationException("Product not found ");
//             }
//         }
//     }
// }
