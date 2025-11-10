using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAdvancedApi.Models;
using MyAdvancedApi.Data;
using System.Security.Claims;
using MyAdvancedApi.Services;
using Microsoft.EntityFrameworkCore;

namespace MyAdvancedApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;

        public UsersController(AppDbContext context,UserService userService){
         _context = context;
         _userService=userService;
        }
        
        [HttpGet("me")]
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
        
            return Ok(new { 
                MyId = userId, 
            });
        }
        
        
        
    }
}
