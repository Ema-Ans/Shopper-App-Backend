using System;


namespace Shopper_Demo.Entities
{
    //  we are using a record instead of a class to represent our order
    public record Order
    {
        // using init instead of set, to ensure it's only set once
        public Guid OrderId {get; init;}
        public string? ShopperName {get; set;}
        // TODO: we will get the name and Id from the shopper entity
        public Guid? Shopper_Id {get; set;}
        public decimal TotalPrice { get; init; }

        public int TotalItems{ get; init; }

        public int TotalProducts{ get; init; }

        public DateTimeOffset CreatedDate {get; init;}
        // to check whether the item is inStock or not
        public bool InProgress {get; set;}

        // Collection of order items associated with the order
        public List<OrderItem> OrderItemsList { get; init; }


    }
}