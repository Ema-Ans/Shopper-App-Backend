using System;
using System.Collections.Generic;
using System.Linq;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;


namespace Shopper_Demo.Repositories
{

    public class InMemoryOrder : IInMemoryOrder
    {
        private readonly List<Order> orderlist = new()
        {
            new Order
                {
                    OrderId = Guid.NewGuid(),
                    ShopperName = "Eman",
                    Shopper_Id = Guid.NewGuid(), // TODO: get the shopper Id from the dictionary
                    TotalPrice = 100, // TODO: write function that calculates this
                    TotalItems = 15, // TODO: write function that calculates this
                    TotalProducts = 5, // TODO: write function that calculates this
                    CreatedDate = DateTimeOffset.UtcNow,
                    InProgress = true, // TODO: write function for this
                    OrderItemsList = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ItemId = Guid.NewGuid(),
                            Name = "Shoes",
                            Price = 20,
                            Quantity = 3,
                            CreatedDate = DateTimeOffset.UtcNow,
                            InStock = true,
                            ProductId = Guid.NewGuid()
                        },
                        new OrderItem
                        {
                            ItemId = Guid.NewGuid(),
                            Name = "Socks",
                            Price = 5,
                            Quantity = 10,
                            CreatedDate = DateTimeOffset.UtcNow,
                            InStock = true,
                            ProductId = Guid.NewGuid()
                        }
                    }
                },
                new Order
                {
                    OrderId = Guid.NewGuid(),
                    ShopperName = "Nayef",
                    Shopper_Id = Guid.NewGuid(),
                    TotalPrice = 200,
                    TotalItems = 10,
                    TotalProducts = 3,
                    CreatedDate = DateTimeOffset.UtcNow,
                    InProgress = false,
                    OrderItemsList = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ItemId = Guid.NewGuid(),
                            Name = "Glasses",
                            Price = 50,
                            Quantity = 2,
                            CreatedDate = DateTimeOffset.UtcNow,
                            InStock = true,
                            ProductId = Guid.NewGuid()
                        },
                        new OrderItem
                        {
                            ItemId = Guid.NewGuid(),
                            Name = "Hat",
                            Price = 30,
                            Quantity = 1,
                            CreatedDate = DateTimeOffset.UtcNow,
                            InStock = true,
                            ProductId = Guid.NewGuid()
                        }
                    }         
                }
        };

        public void AssignOrder(Guid OrderId, Guid ShopperId)
        {
            var order = orderlist.SingleOrDefault(o => o.OrderId == OrderId);
            if (order != null)
            {
                order.Shopper_Id = ShopperId;
            }
            else
            {
                throw new InvalidOperationException("Order not found");
            }        
            
        }

        public void CompleteOrder(Guid OrderId, Guid ShopperId)
        {
            var order = orderlist.SingleOrDefault(o => o.OrderId == OrderId && o.Shopper_Id == ShopperId);
            if (order != null)
            {
                order.InProgress = false;
            }
            else
            {
                throw new InvalidOperationException("Order not found or shopper mismatch");
            }        }

        public void CreateOrder(Order ord)
        {
            orderlist.Add(ord);
        }

        public void DeleteOrder(Guid OrderId)
        {
            var index = orderlist.FindIndex(existingOrder => existingOrder.OrderId == OrderId);
            if (index != -1)
            {
                orderlist.RemoveAt(index);
            }
        }

        public Order? GetOrderbyShopperId(Guid ShopperId)
        {
             return orderlist.SingleOrDefault(o => o.Shopper_Id == ShopperId);
        }

        
        public Order? GetOrderById(Guid Id)
        {
             return orderlist.SingleOrDefault(o => o.OrderId == Id);
        }


        public IEnumerable<Order> GetOrders()
        {
            return orderlist;
        }

        public void UpdateOrder(Order ord)
        {
            var index = orderlist.FindIndex(existingOrder => existingOrder.OrderId == ord.OrderId);
            if (index != -1)
            {
                orderlist[index] = ord;
            }
        }
    }

}