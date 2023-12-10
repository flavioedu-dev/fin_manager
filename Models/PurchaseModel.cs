using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace fin_manager.Models
{
    public class PurchaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }
        
        [BsonElement("place")]
        [Required]
        public string Place { get; set; }

        [BsonElement("totalValue")]
        public double TotalValue
        {
            get
            {
                return TotalPurchasePrice();
            }
        }
        
        [BsonElement("products")]
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();


        public PurchaseModel(string place, string title = "Compra")
        {
            Place = place;
            Title = title;
        }


        public void AddProduct (ProductModel product)
        {
            Products.Add(product);
        }

        public double TotalPurchasePrice ()
        {
            double total = 0;
            foreach (ProductModel product in Products)
            {
                total += product.TotalPrice();
            }

            return total;
        }
    }
}
