using Shopper_Demo.DTOs;
using Shopper_Demo.Entities;

namespace Shopper_Demo
{
    //use static classes for extensions
    public static class Extensions 
    {
        public static OrderItemDto AsDto(this OrderItem item)
        {
            return new OrderItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                CreatedDate = item.CreatedDate,
                InStock = item.InStock,
                ProductId = item.ProductId
            };
        }


        public static ProductDto AsProdDto(this Product prod)
        {

            return new ProductDto
            {
                ProdId = prod.ProdId,
                Name  = prod.Name,
                Description = prod.Description,
                Category = prod.Category,
                Brand = prod.Brand,
                QuantityAvailable = prod.QuantityAvailable,
                CostPrice = prod.CostPrice,
                SellingPrice = prod.SellingPrice,
                DateAdded = prod.DateAdded,
                ExpirationDate = prod.ExpirationDate
            };
        }

        public static OrderDto AsOrderDto(this Order ord)
        {

            return new OrderDto
            {
                OrderId = ord.OrderId,
                ShopperName = ord.ShopperName,
                Shopper_Id = ord.Shopper_Id,
                TotalPrice = ord.TotalPrice,
                TotalItems = ord.TotalItems,
                TotalProducts = ord.TotalProducts,
                CreatedDate = ord.CreatedDate,
                InProgress = ord.InProgress,
                OrderItemsList = ord.OrderItemsList
            };
        }

        public static ShopperDto AsShopperDto(this Shopper shop)
        {

            return new ShopperDto
            {
                ShopperId = shop.ShopperId,
                ShopperName = shop.ShopperName,
                UserName = shop.UserName,
                Email = shop.Email,
                PhoneNumber = shop.PhoneNumber,
                OrdersAssigned = shop.OrdersAssigned,
                PastOrders = shop.PastOrders,
                CreatedDate = shop.CreatedDate
            };
        }
    }
}