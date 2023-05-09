using System.ComponentModel.DataAnnotations;

namespace VK_test.Models
{
    public class UserDTO
    {

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
