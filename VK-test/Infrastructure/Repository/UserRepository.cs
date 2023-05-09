using Infrastructure.Context;
using Infrastructure.Enums;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public override async Task<Users> AddAsync(Users user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            await UserExistsAsync(user.Login, user.UsersGroup?.Code);

            await Task.Delay(1000);

            var lokcObject = new object();

            try
            {
                lock (lokcObject)
                {
                    _ = UserExistsAsync(user.Login, user.UsersGroup?.Code);

                    user.UsersGroupId = (int)UserGroupCode.User;
                    user.UsersStateId = (int)UserStateCode.Active;

                    _context.User.Add(user);
                    _context.SaveChanges();

                    transaction.CommitAsync();
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            return user;
        }

        public override async Task UpdateAsync(Users user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            await UserExistsAsync(user.Login, user.UsersGroup!.Code);

            var lokcObject = new object();

            try
            {
                lock (lokcObject)
                {
                    _ = UserExistsAsync(user.Login, user.UsersGroup!.Code);

                    _entities.Update(user);
                    _context.SaveChanges();

                    transaction.Commit();
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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


        /// <summary>
        /// Проверка на дубликаты
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private async Task UserExistsAsync(string login, UserGroupCode? groupCode = UserGroupCode.User)
        {
            var checkLoginUser = await _context.User.AnyAsync(u => u.Login == login);
            if (checkLoginUser)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }
            if (groupCode == UserGroupCode.Admin)
            {
                var userExistsRoleAdmin = await _context.User.AnyAsync(u => u.UsersGroup!.Code == UserGroupCode.Admin);
                if (userExistsRoleAdmin)
                {
                    throw new Exception("Два админа не может быть в системе");
                }
            }
        }

        /// <summary>
        /// Получение пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Users?> GetUser(string login, string password)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }
    }
}