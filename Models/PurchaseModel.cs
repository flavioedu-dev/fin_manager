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
        public string Title { get; set; } = "Compra";
        
        [BsonElement("place")]
        [Required]
        public string Place { get; set; }
        
        [BsonElement("products")]
        public List<ProductModel> Products { get; set; }


        public PurchaseModel(string title, string place)
        {
            Title = title;
            Place = place;
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
                total += product.Price * product.Amount;
            }

            return total;
        }
    }
}
