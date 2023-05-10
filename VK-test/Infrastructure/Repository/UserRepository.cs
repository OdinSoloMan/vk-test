using Infrastructure.Context;
using Infrastructure.Enums;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repository
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        private readonly IMemoryCache _cache;

        public UserRepository(
            ApplicationDbContext context,
            IMemoryCache cache) : base(context)
        {
            _cache = cache;
        }

        public override async Task<Users> AddAsync(Users user)
        {
            if (_cache.TryGetValue(user.Login, out DateTime when))
            {
                var seconds = (DateTime.Now - when).TotalSeconds;
                if (seconds < 5)
                {
                    throw new Exception("Пользователь с таким же логином уже регистрируется");
                }
            }

            _cache.Set(user.Login, DateTime.Now);

            await Task.Delay(1000);

            using var transaction = _context.Database.BeginTransaction();

            UserExists(user.Login, user.UsersGroup?.Code);

            var lockObject = new object();

            try
            {
                lock (lockObject)
                {
                    UserExists(user.Login, user.UsersGroup?.Code);

                    user.UsersGroupId = (int)UserGroupCode.User;
                    user.UsersStateId = (int)UserStateCode.Active;

                    _context.User.Add(user);
                    _context.SaveChanges();

                    transaction.Commit();
                }
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            return user;
        }

        public override async Task DeleteAsync(int id)
        {
            var user = await _entities.FindAsync(id);
            if (user != null)
            {
                user.UsersStateId = (int)UserStateCode.Blocked;
                _entities.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Users?> GetUser(string login, string password)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }

        public async Task ChangeAdmin(int newAdmin)
        {
            await Task.Run(() =>
            {
                using var transaction = _context.Database.BeginTransaction();

                var lockObject = new object();

                try
                {
                    lock (lockObject)
                    {
                        var newAdminSystem = _entities.Include(m => m.UsersGroup).FirstOrDefault(x => x.Id == newAdmin);

                        if (newAdminSystem == null)
                        {
                            throw new Exception("Такого пользователя нету");
                        }

                        var oldAdmin = _entities.Include(m => m.UsersGroup).FirstOrDefault(x => x!.UsersGroup!.Code == UserGroupCode.Admin);


                        if (oldAdmin != null)
                        {
                            oldAdmin!.UsersGroupId = (int)UserGroupCode.User;
                            _entities.Update(oldAdmin);
                        }

                        newAdminSystem!.UsersGroupId = (int)UserGroupCode.Admin;
                        _entities.Update(newAdminSystem);

                        _context.SaveChanges();

                        transaction.Commit();
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            });
        }

        /// <summary>
        /// Проверка на дубликаты и администратора
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="groupCode">UserGroupCode пользователя</param>
        /// <exception cref="Exception">Выдаст ошибку если нет соблюдения правил</exception>
        private void UserExists(string login, UserGroupCode? groupCode = UserGroupCode.User)
        {
            var checkLoginUser = _context.User.Any(u => u.Login == login);
            if (checkLoginUser)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }
            if (groupCode == UserGroupCode.Admin)
            {
                var userExistsRoleAdmin = _context.User.Any(u => u.UsersGroup!.Code == UserGroupCode.Admin);
                if (userExistsRoleAdmin)
                {
                    throw new Exception("Два админа не может быть в системе");
                }
            }
        }
    }
}