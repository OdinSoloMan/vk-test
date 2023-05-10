namespace VK_test.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; } = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Дата создание учетной записи
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string UserGroup { get; set; } = string.Empty;
        /// <summary>
        /// Состояние учетной пользователя (активная/заблокирования)
        /// </summary>
        public string UserState { get; set; } = string.Empty;
    }
}
