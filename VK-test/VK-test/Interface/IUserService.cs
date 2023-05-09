using Infrastructure.Models;
using VK_test.Models;

namespace VK_test.Interface
{
    public interface IUserService
    {
        Task<Users> CreateUser(UserDTO userDTO);
        Task<string> Login(UserDTO userDTO);
        Task<UserInfo> GetUser(int  id);
        Task<IQueryable<UserInfo>> GetUsers();
        Task DeleteUser(int id);
    }
}
