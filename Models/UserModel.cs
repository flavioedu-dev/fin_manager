using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace fin_manager.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("email")]
        [Required]
        public string Email { get; set; }

        [BsonElement("password")]
        [Required]
        public string Password { get; set; }

        [BsonElement("purchases")]
        public List<string> Purchases { get; private set; } = new List<string>();

        public void AddPurchaseToUser(string idPurchase)
        {
            Purchases.Add(idPurchase);
        }
    }
}
