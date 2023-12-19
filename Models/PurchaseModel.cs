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
        public double TotalValue { get; private set; }

        [BsonElement("products")]
        public List<string> Products { get; private set; } = new List<string>();


        public PurchaseModel(string place, string title = "Compra")
        {
            Place = place;
            Title = title;
        }


        public void AddProduct(string idProduct)
        {
            Products.Add(idProduct);
        }

        public void TotalPurchasePrice(List<ProductModel> products)
        {
            double total = 0;
            foreach (ProductModel product in products)
            {
                total += product.TotalPrice();
            }

            TotalValue = total;
        }
    }
}
