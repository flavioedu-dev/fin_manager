using MongoDB.Driver;
using fin_manager.Models;
using fin_manager.Models.Interfaces;
using fin_manager.Services.Interfaces;

namespace fin_manager.Services
{
    public class ProductService : IProductService 
    {
        private readonly IMongoCollection<ProductModel> _products;

        public ProductService(IMongoConfiguration mongoConfig, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoConfig.DatabaseName);
            _products = database.GetCollection<ProductModel>("products");
        }

        public List<ProductModel> GetProducts()
        {
            try
            {
                return _products.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public ProductModel GetProductById(string id)
        {
            try
            {
                return _products.Find(x => x.Id == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public List<ProductModel> GetManyProducts(List<string> productsId)
        {
            try
            {
                List<ProductModel> purchaseProducts = new List<ProductModel>();
                foreach (string productId in productsId)
                {
                    ProductModel productExists = _products.Find(x => x.Id == productId).FirstOrDefault();
                    if (productExists == null) throw new Exception($"Product with id {productId} not found.");
                    
                    purchaseProducts.Add(productExists);
                }

                if (purchaseProducts.Count != productsId.Count) throw new Exception("Error when finding products.");

                return purchaseProducts;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public ProductModel CreateProduct(ProductModel product)
        {
            try
            {
                _products.InsertOne(product);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public void UpdateProduct(string id, ProductModel product)
        {
            try
            {
                _products.ReplaceOne(x => x.Id == id, product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public void DeleteProduct(string id)
        {
            try
            {
                _products.DeleteOne(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }
    }
}
