using Infrastructure.Enums;

namespace Infrastructure.Models
{
    public class UsersGroup: BaseEntity
    {
        public UserGroupCode Code { get; set; } = UserGroupCode.User;
        public string Description { get; set; } = string.Empty;
    }
}
