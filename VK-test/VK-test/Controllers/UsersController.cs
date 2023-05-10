using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VK_test.Interface;
using VK_test.Models;

namespace VK_test.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Конструктор UsersController
        /// </summary>
        /// <param name="userService">IUserService</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Добавление/Создание пользователя
        /// </summary>
        /// <param name="userDTO">Модель для добавления пользователя в систему</param>
        /// <returns>Возращение не полной модели пользователя</returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<Users> Create([FromBody] UserDTO userDTO)
        {
            return await _userService.CreateUser(userDTO);
        }

        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="userDTO">Модель для входа в систему</param>
        /// <returns>Модель для получения токена</returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<TokenDTO> Login([FromBody] UserDTO userDTO)
        {
            return await _userService.Login(userDTO);
        }

        /// <summary>
        /// Получение пользователей
        /// </summary>
        /// <param name="pagination">Модель для пагинации</param>
        /// <returns>Результат фильтрации данных по страницам</returns>
        [HttpGet("[action]")]
        public async Task<PaginatedList<UserInfo>> GetUsers([FromQuery] PaginationModel pagination)
        {
            return await _userService.GetUsers(pagination);
        }

        /// <summary>
        /// Получение пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Получение выбранного пользователя</returns>
        [HttpGet("[action]/{id}")]
        public async Task<UserInfo> GetUser([FromRoute] int id)
        {
            return await _userService.GetUser(id);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Получение сообщения об удаление</returns>
        [HttpDelete("[action]/{id}")]
        public async Task<MessageDTO> DeleteUser([FromRoute] int id)
        {
            return await _userService.DeleteUser(id);
        }

        /// <summary>
        /// Смена админа
        /// </summary>
        /// <param name="newAdmin">Id нового администратора</param>
        /// <returns>Получение сообщения об смене администратора</returns>
        [HttpPut("[action]/{newAdmin}")]
        public async Task<MessageDTO> ChangeAdmin([FromRoute] int newAdmin)
        {
            return await _userService.ChangeAdmin(newAdmin);
        }
    }
}
