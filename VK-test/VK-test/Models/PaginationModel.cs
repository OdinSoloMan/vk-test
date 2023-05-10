namespace VK_test.Models
{
    /// <summary>
    /// Пагинация
    /// </summary>
    public class PaginationModel
    {
        private int pageNumber = 1;
        private int pageSize = 5;

        /// <summary>
        /// Текущая страница
        /// </summary>
        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                if (value > 0)
                {
                    pageNumber = value;
                }
                else
                {
                    pageNumber = 1;
                }
            }
        }

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value > 0)
                {
                    pageSize = value;
                }
                else
                {
                    pageSize = 5;
                }
            }
        }
    }
}
