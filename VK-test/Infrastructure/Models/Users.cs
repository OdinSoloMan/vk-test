namespace Infrastructure.Models
{
    public class Users : BaseEntity
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        
        public int UsersGroupId { get; set; }
        public UsersGroup? UsersGroup { get; set; }

        public int UsersStateId { get; set; }
        public UsersState? UsersState { get; set; }
    }
}
