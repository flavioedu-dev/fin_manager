using fin_manager.Models;
using fin_manager.Services.Interfaces;
using MongoDB.Driver;
using fin_manager.Models.Interfaces;

namespace fin_manager.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<UserModel> _users;

        public UserService(IMongoConfiguration mongoConfig, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoConfig.DatabaseName);
            _users = database.GetCollection<UserModel>("users");
        }

        public UserModel CreateUser(UserModel user)
        {
            try
            {
                _users.InsertOne(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public UserModel GetUserById(string id)
        {
            try
            {
                return _users.Find(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public UserModel GetUserByEmail(string email)
        {
            try
            {
                return _users.Find(x => x.Email == email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public List<UserModel> GetUsers()
        {
            try
            {
                return _users.Find(_ => true).ToList<UserModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }

        public void UpdateUser(string id, UserModel user)
        {
            try
            {
                _users.ReplaceOne(x => x.Id == id, user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }
        public void DeleteUser(string id)
        {
            try
            {
                _users.DeleteOne(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: ${ex.Message}");
            }
        }
    }
}
