using System.ComponentModel.DataAnnotations;

namespace Shopper_Demo.DTOs
{
    public record  CreateProductDto
    {
        
        // using init instead of set, to ensure it's only set once
        // public  Guid ProdId { get; init; } // Unique identifier for the product
        [Required]
        public string? Name { get; init; } // Name of the product
        public string? Description { get; set; } // Description of the product
        // TODO: could probably make a function that assigns this by default
        public string? Category { get; set; } // Category of the product
        [Required]
        public string? Brand { get; set; } // Brand of the product
        [Required]
        public int QuantityAvailable { get; set; } // Quantity available for sale
        public decimal CostPrice { get; set; } // Cost price of the product
        [Required]
        public decimal SellingPrice { get; set; } // Selling price of the product
        // public DateTimeOffset DateAdded { get; init; } // Date when the product was added
        public DateTime? ExpirationDate { get; set; } // Expiration date for perishable goods

    }
}