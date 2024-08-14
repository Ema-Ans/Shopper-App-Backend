
using Shopper_Demo.Entities;


namespace Shopper_Demo.DTOs
{
    public record UpdateShopperDto
    {
        // public Guid ShopperId { get; init; }
        public string? ShopperName { get; init; }
        public string? UserName { get; init; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        // public AddressModel Address { get; set; }
        // public List<Order> OrdersAssigned { get; set; }
        // public List<Order> PastOrders { get; set; }
        // public DateTimeOffset CreatedDate {get; init;}
    }

    // public class AddressModel
    // {
    //     public string Street1 { get; set; }
    //     public string? Street2 { get; set; }
    //     public string City { get; set; }
    //     public string Country { get; set; }
    // }
}
