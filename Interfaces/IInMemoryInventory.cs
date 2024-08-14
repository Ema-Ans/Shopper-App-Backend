using Shopper_Demo.Entities;

namespace Shopper_Demo.Interfaces
{
    public interface IInMemoryInventory
    {
        Dictionary<string, Guid> ProductIdMap { get; }
        Product? GetProduct(Guid Id);

        IEnumerable<Product> GetProducts();

        void CreateProduct(Product prod);
        void UpdateProduct(Product prod);
        void DeleteProduct(Guid Id);

        void DecreaseQuantity(Guid Id, int quantity);

        void IncreaseQuantity(Guid Id, int quantity);
        
    }
}