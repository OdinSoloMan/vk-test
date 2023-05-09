using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VK_test.Interface;
using VK_test.Models;

namespace VK_test.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<Users> Create([FromBody] UserDTO userDTO)
        {
            return await _userService.CreateUser(userDTO);
        }
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<string> Login([FromBody] UserDTO userDTO)
        {
            return await _userService.Login(userDTO);
        }
    }
}
