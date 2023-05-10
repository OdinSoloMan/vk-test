using Infrastructure.Models;
using VK_test.Models;

namespace VK_test.Interface
{
    /// <summary>
    /// IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userDTO">Модель UserDTO</param>
        /// <returns>Информация о пользователи</returns>
        Task<Users> CreateUser(UserDTO userDTO);
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="userDTO">Модель UserDTO</param>
        /// <returns>Токен</returns>
        Task<TokenDTO> Login(UserDTO userDTO);
        /// <summary>
        /// Получение пользователя по id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Получение полной информации о пользователе</returns>
        Task<UserInfo> GetUser(int id);
        /// <summary>
        /// Получение списка пользователя
        /// </summary>
        /// <param name="pagination">Пагинация списка</param>
        /// <returns>Отфильтрованный список</returns>
        Task<PaginatedList<UserInfo>> GetUsers(PaginationModel pagination);
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Результат удаления</returns>
        Task<MessageDTO> DeleteUser(int id);
        /// <summary>
        /// Смена администратора
        /// </summary>
        /// <param name="newAdmin">id пользователя</param>
        /// <returns>Результат смены</returns>
        Task<MessageDTO> ChangeAdmin(int newAdmin);
    }
}
