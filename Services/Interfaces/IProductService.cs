using fin_manager.Models;

namespace fin_manager.Services.Interfaces
{
    public interface IProductService
    {
        public List<ProductModel> GetProducts();
        public ProductModel GetProductById(string id);
        public List<ProductModel> GetPurchaseProducts(List<string> productsId);
        public ProductModel CreateProduct(ProductModel product);
        public void UpdateProduct(string id, ProductModel product);
        public void DeleteProduct(string id);
    }
}
