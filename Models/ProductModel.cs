using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace fin_manager.Models
{
    public class ProductModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("price")]
        public double Price { get; set; } = 0.0;

        [BsonElement("amount")]
        public int Amount { get; set; } = 0;


        public ProductModel(string name, double price, int amount)
        {
            Name = name;
            Price = price;
            Amount = amount;
        }


        public double TotalPrice ()
        {
            return Price * Amount;
        }
    }
}
