using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using Shopper_Demo.Entities;
using Shopper_Demo.Interfaces;


namespace Shopper_Demo.Repositories
{

    
    public class InMemoryShopper : IInMemoryShopper
    {

        private readonly List<Shopper> shopperlist = new()
        {
            // TODO: fix the mock data here
            new Shopper
            {
                ShopperId = Guid.NewGuid(),
                ShopperName = "Eman",
                UserName = "eman123",
                PasswordHash = "password1", // Assuming PasswordHelper class exists for hashing
                Email = "eman@example.com",
                PhoneNumber = "123-456-7890",
                // Address = new AddressModel
                // {
                //     Street1 = "123 Main St",
                //     Street2 = "Apt 1",
                //     City = "Doha",
                //     Country = "Qatar"
                // },
                OrdersAssigned = new List<Order>
                {
                    new Order
                    {
                        OrderId = Guid.NewGuid(),
                        ShopperName = "Eman",
                        Shopper_Id = Guid.NewGuid(),
                        TotalPrice = 100,
                        TotalItems = 15,
                        TotalProducts = 5,
                        CreatedDate = DateTimeOffset.UtcNow,
                        InProgress = true,
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
                    }
                },
                PastOrders = new List<Order>()
            },
            new Shopper
            {
                ShopperId = Guid.NewGuid(),
                ShopperName = "Nayef",
                UserName = "nayef123",
                PasswordHash = "password2", // Assuming PasswordHelper class exists for hashing
                Email = "nayef@example.com",
                PhoneNumber = "987-654-3210",
                // Address = new AddressModel
                // {
                //     Street1 = "456 Secondary St",
                //     City = "Doha",
                //     Country = "Qatar"
                // },
                OrdersAssigned = new List<Order>
                {
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
                },
                PastOrders = new List<Order>()
            }
        };


        public void CreateShopper(Shopper shop) => shopperlist.Add(shop);
    
        public void DeleteShopper(Guid shopperid)
        {
            var index = shopperlist.FindIndex(existingShopper => existingShopper.ShopperId == shopperid);
            if (index != -1) shopperlist.RemoveAt(index);
        }

        public void DeleteShopper(Shopper shop)
        {
            throw new NotImplementedException();
        }

        // Todo: maybe they need to be moved to the service layer for shopper
        public IEnumerable<Order> GetOrdersCompleted(Guid ShopperId)
        {
            var shopper = shopperlist.SingleOrDefault(s => s.ShopperId == ShopperId);
            if (shopper == null)
                throw new InvalidOperationException("Shopper not found");

            return shopper.PastOrders.Where(o => !o.InProgress).ToList();
        }

        public IEnumerable<Order> GetPastOrders(Guid ShopperId)
        {
            var shopper = shopperlist.SingleOrDefault(s => s.ShopperId == ShopperId);
            if (shopper == null)
                throw new InvalidOperationException("Shopper not found");

            return shopper.PastOrders;
        }
        

        public Shopper GetShopperById(Guid ShopperId)
        {
            return shopperlist.SingleOrDefault(s => s.ShopperId == ShopperId) ?? throw new InvalidOperationException("Shopper not found");
        }


        public IEnumerable<Shopper> GetShoppers() => shopperlist;
        public void UpdateShopper(Shopper shop)
        {
            var index = shopperlist.FindIndex(existingShopper => existingShopper.ShopperId == shop.ShopperId);
            if (index != -1) shopperlist[index] = shop;
        }
    }

}