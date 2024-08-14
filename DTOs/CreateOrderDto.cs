using Shopper_Demo.Entities;

namespace Shopper_Demo.DTOs
{
    public record CreateOrderDto
    {
        // public Guid OrderId {get; init;}
        // public string? ShopperName {get; init;}
        // TODO: we will get the name and Id from the shopper entity
        // public Guid? Shopper_Id {get; set;}
        // public decimal TotalPrice { get; init; }

        // public int TotalItems{ get; init; }

        // public int TotalProducts{ get; init; }

        // public DateTimeOffset CreatedDate {get; init;}
        // to check whether the item is inStock or not
        // public bool InProgress {get; set;}

        // Collection of order items associated with the order
        public List<OrderItem> OrderItemsList { get; init; }
    }
}