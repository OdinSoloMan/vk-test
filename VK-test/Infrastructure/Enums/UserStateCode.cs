using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Infrastructure.Enums
{
    public enum UserStateCode
    {
        [Display(Name = "Active")]
        Active = 1,
        [Display(Name = "Blocked")]
        Blocked = 2,
    }
}
