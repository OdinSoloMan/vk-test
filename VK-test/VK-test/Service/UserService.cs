using AutoMapper;
using Infrastructure.Enums;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VK_test.Interface;
using VK_test.Models;

namespace VK_test.Service
{
    /// <summary>
    /// UserService
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор UserService
        /// </summary>
        /// <param name="userRepository">IUserRepository</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="mapper">IMapper</param>
        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userDTO">Модель UserDTO</param>
        /// <returns>Информация о пользователи</returns>
        public async Task<Users> CreateUser(UserDTO userDTO)
        {
            var user = new Users { Login = userDTO.Username, Password = PasswordHelper.EncodePasswordToBase64(userDTO.Password) };
            return await _userRepository.AddAsync(user);
        }

        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="userDTO">Модель UserDTO</param>
        /// <returns>Токен</returns>
        public async Task<TokenDTO> Login(UserDTO userDTO)
        {
            var user = await _userRepository.Where(x =>
                    x.Login == userDTO.Username && x.Password == PasswordHelper.EncodePasswordToBase64(userDTO.Password)
                ).Include(m => m.UsersGroup).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Указаны не верные данные");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user?.UsersGroup?.Code.ToString() ?? UserGroupCode.User.ToString()),
                new Claim(ClaimTypes.Sid, user!.Id.ToString()),
            };

            var token = new JwtSecurityToken
            (
                claims: claims,
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "")),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return tokenString;
        }

        /// <summary>
        /// Получение списка пользователя
        /// </summary>
        /// <param name="pagination">Пагинация списка</param>
        /// <returns>Отфильтрованный список</returns>
        public async Task<PaginatedList<UserInfo>> GetUsers(PaginationModel pagination)
        {
            var users = await _userRepository.GetAllAsync();

            var totalItems = users.Count();

            var paginatedList = new PaginatedList<UserInfo>
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize),
                TotalItems = totalItems,
            };

            paginatedList.Items = _mapper.ProjectTo<UserInfo>(
                    users
                        ?.OrderBy(x => x.Id)
                        ?.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                        ?.Take(pagination.PageSize)

                ).ToList();

            return paginatedList;
        }

        /// <summary>
        /// Получение пользователя по id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Получение полной информации о пользователе</returns>
        public async Task<UserInfo> GetUser(int id)
        {
            var user = await _userRepository.Where(x => x.Id == id)
                .Include(m => m.UsersGroup)
                .Include(m => m.UsersState)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<UserInfo>(user);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Результат удаления</returns>
        public async Task<MessageDTO> DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
            return new MessageDTO() { Message = $"Пользователь удален {id}" };
        }

        /// <summary>
        /// Смена администратора
        /// </summary>
        /// <param name="newAdmin">id пользователя</param>
        /// <returns>Результат смены</returns>
        public async Task<MessageDTO> ChangeAdmin(int newAdmin)
        {
            await _userRepository.ChangeAdmin(newAdmin);

            return new MessageDTO() { Message = $"Назначен новый администратор {newAdmin}" };
        }
    }
}
