using AutoMapper;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using VK_test.Interface;
using VK_test.Models;

namespace VK_test.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Users> CreateUser(UserDTO userDTO)
        {
            var user = new Users { Login = userDTO.Username, Password = EncodePasswordToBase64(userDTO.Password) };
            return await _userRepository.AddAsync(user);
        }

        public async Task<string> Login(UserDTO userDTO)
        {
            var user = await _userRepository.GetUser(userDTO.Username, EncodePasswordToBase64(userDTO.Password));

            if (user == null)
            {
                throw new Exception("Указаны не верные данные");
            }

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "")),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public async Task<IQueryable<UserInfo>> GetUsers()
        {
            return _mapper.ProjectTo<UserInfo>(
                    await _userRepository.GetAllAsync()
                );
        }

        public async Task<UserInfo> GetUser(int id)
        {
            var user = await _userRepository.Where(x => x.Id == id)
                .Include(m => m.UsersGroup)
                .Include(m => m.UsersState)
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<UserInfo>(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка в base64Encode" + ex.Message);
            }
        }
    }
}
