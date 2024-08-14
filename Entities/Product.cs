namespace Shopper_Demo.Entities
{
    public record Product
    {
        public  Guid ProdId { get; init; } // Unique identifier for the product
        public string Name { get; init; } // Name of the product
        public string Description { get; set; } // Description of the product
        public string Category { get; set; } // Category of the product
        public string Brand { get; set; } // Brand of the product
        public int QuantityAvailable { get; set; } // Quantity available for sale
        public decimal CostPrice { get; set; } // Cost price of the product
        public decimal SellingPrice { get; set; } // Selling price of the product
        public DateTimeOffset DateAdded { get; init; } // Date when the product was added
        public DateTime? ExpirationDate { get; set; } // Expiration date for perishable goods
    }
}