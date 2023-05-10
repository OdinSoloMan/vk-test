using System.ComponentModel.DataAnnotations;

namespace VK_test.Models
{
    /// <summary>
    /// Модель для пользователя при входе/регистрации
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Логин/имя пользователя
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
