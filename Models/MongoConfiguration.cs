using fin_manager.Models.Interfaces;

namespace fin_manager.Models
{
    public class MongoConfiguration : IMongoConfiguration
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
