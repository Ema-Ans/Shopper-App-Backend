namespace Shopper_Demo.DTOs
{
    public record OrderItemDto
    {
        
        // using init instead of set, to ensure it's only set once
        public Guid ItemId {get; init;}
        public string? Name {get; init;}

        public decimal Price {get; init;}

        public int Quantity {get; set;}

        public DateTimeOffset CreatedDate {get; init;}
        // to check whether the item is inStock or not
        public bool InStock {get; set;}

        public Guid ProductId { get; set; } // Reference to the product in the inventory


    }
}