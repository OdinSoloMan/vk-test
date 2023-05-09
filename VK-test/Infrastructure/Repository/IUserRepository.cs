using Infrastructure.Enums;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Task<Users?> GetUser(string login, string password);
    }
}
