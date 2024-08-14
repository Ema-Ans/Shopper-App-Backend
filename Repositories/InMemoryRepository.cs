using System;
using System.Collections.Generic;
using System.Linq;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;

namespace Shopper_Demo.Repositories
{
    public class InMemoryRepository : IInMemoryRepository
    {
        private readonly List<OrderItem> orderitems;
        private readonly Dictionary<string, Guid> productIdMap;

        public InMemoryRepository(Dictionary<string, Guid> productIdMap)
        {
            this.productIdMap = productIdMap;
            // TODO: need to deal with Pricing stuff
            // TODO: need to deal with missing keys and productIDs
            // TODO: need to enusre that only products that exist in inventory are added
            orderitems = new List<OrderItem>
            {
                new OrderItem
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Shoes",
                    Price = 9,
                    CreatedDate = DateTimeOffset.UtcNow,
                    Quantity = 10,
                    InStock = true,
                    ProductId = productIdMap["Shoes"]
                },
                new OrderItem
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Bags",
                    Price = 5,
                    CreatedDate = DateTimeOffset.UtcNow,
                    Quantity = 10,
                    InStock = true,
                    ProductId = productIdMap["Bags"]
                },
                new OrderItem
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Makeup",
                    Price = 7,
                    CreatedDate = DateTimeOffset.UtcNow,
                    Quantity = 10,
                    InStock = true,
                    ProductId =  productIdMap["Makeup"]
                }
            };
        }

        public IEnumerable<OrderItem> GetOrderItems() => orderitems;

        public OrderItem? GetOrderItem(Guid id) => orderitems.SingleOrDefault(item => item.ItemId == id);

        public void CreateOrderItem(OrderItem item) => orderitems.Add(item);

        public void UpdateOrderItem(OrderItem item)
        {
            var index = orderitems.FindIndex(existingItem => existingItem.ItemId == item.ItemId);
            if (index != -1) orderitems[index] = item;
        }

        public void DeleteOrderItem(Guid id)
        {
            var index = orderitems.FindIndex(existingItem => existingItem.ItemId == id);
            if (index != -1) orderitems.RemoveAt(index);
        }
    }
}
