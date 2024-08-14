using System.ComponentModel.DataAnnotations;

namespace Shopper_Demo.DTOs
{
    public record  CreateOrderItemDto
    {
        
        // using init instead of set, to ensure it's only set once
        // public Guid ItemId {get; init;}
        [Required]
        public string? Name {get; init;}
        [Required]
        //TODO: may need to remove the range
        // [Range(1, 100000)]
        public decimal Price {get; init;}

        public int Quantity {get; set;}
        public Guid ProductId { get; set; } // Reference to the product in the inventory

        // public DateTimeOffset CreatedDate {get; init;}
        // to check whether the item is inStock or not
        // public bool InStock {get; set;}

    }
}