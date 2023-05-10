namespace VK_test.Models
{
    /// <summary>
    /// PaginatedList
    /// </summary>
    /// <typeparam name="T">Модель для списка</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Всего страниц
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Всего элементов
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Список элементов
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();
    }
}
