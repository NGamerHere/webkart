using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCart.Services;

namespace WebCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService){
         _userService=userService;
        }
        
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User not found");
            }

            var user = await _userService.GetUserById(int.Parse(userId));
            if (user == null)
            {
                return Unauthorized("User not found");
            }
        
            return Ok(new { 
                MyId = userId, 
            });
        }
        
        
        
    }
}
