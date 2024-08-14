using System.ComponentModel.DataAnnotations;

namespace Shopper_Demo.DTOs
{
    public record  CreateShopperDto
    {
        
        // using init instead of set, to ensure it's only set once
        // public Guid ItemId {get; init;}
        [Required]
        public string? ShopperName { get; init; }

        [Required]
        //TODO: may need to add some restrictions on the type of usernames
        // [Range(1, 100000)]
        public string? UserName { get; init; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        // public AddressModel Address { get; set; }
    }
    //  public class AddressModel
    // {
    //     public string Street1 { get; set; }
    //     public string? Street2 { get; set; }
    //     public string City { get; set; }
    //     public string Country { get; set; }
    // }
}
