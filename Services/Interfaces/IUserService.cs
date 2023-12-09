using fin_manager.Models;

namespace fin_manager.Services.Interfaces
{
    public interface IUserService
    {
        public List<UserModel> GetUsers();
        public UserModel GetUserById(string id);
        public UserModel GetUserByEmail(string email);
        public UserModel CreateUser(UserModel user);
        public void UpdateUser(string id, UserModel user);
        public void DeleteUser(string id);
    }
}
