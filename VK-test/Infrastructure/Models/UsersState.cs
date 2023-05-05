using Infrastructure.Enums;

namespace Infrastructure.Models
{
    public class UsersState : BaseEntity
    {
        public UserStateCode Code { get; set; } = UserStateCode.Active;
        public string Description { get; set; } = string.Empty;
    }
}
