namespace fin_manager.Models.Interfaces
{
    public interface IMongoConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
