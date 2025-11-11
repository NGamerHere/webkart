using Microsoft.AspNetCore.Mvc;
using WebCart.Data;
using Microsoft.AspNetCore.Authorization;
using WebCart.DTO;
using WebCart.Enum;
using WebCart.Models;
using WebCart.Services;

namespace WebCart.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase {
    
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public AuthController(UserService userService,TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginInfo loginInfo)
    {
        User? user = await _userService.GetUserByPhoneAndEmail(loginInfo.Login);
        if (user != null && user.PasswordCheck(loginInfo.Password) )
        {
            string token = _tokenService.CreateToken(user);
            return Ok(new { Message = "Login Successful" , token });
        }
        return Unauthorized(new {error = "Invalid username or password."});
    }
    
    [HttpPost("register/{role}")]
    public async Task<IActionResult> Register(RegisterDto registerDto,string role)
    {
        if (role == "Seller" || role == "User" )
        {
            User user = new User {
                Name = registerDto.Name,
                Password = registerDto.Password,
                email = registerDto.email,
                phone = registerDto.phone,
                Role = role == "Seller" ? Role.Seller :  Role.User
            };
            User savedUser = await _userService.createNewUser(user);
            return Ok(new { message = "User created successfully", user = savedUser });
        } else {
          return  BadRequest(new { error = "role is invalid" });
        }
    }


}