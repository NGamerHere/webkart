using Microsoft.AspNetCore.Mvc;
using MyAdvancedApi.Data;
using MyAdvancedApi.DTO;
using Microsoft.AspNetCore.Authorization;
using MyAdvancedApi.Models;
using MyAdvancedApi.Services;

namespace MyAdvancedApi.Controllers;

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
        if (user == null && user.PasswordCheck(loginInfo.Password) )
        {
            return Unauthorized("Invalid username or password.");
        }
        string token = _tokenService.CreateToken(user);
        return Ok(new { Message = "Login Successful" , token });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        User savedUser = await _userService.createNewUser(user);
        return Ok(new { message = "User created successfully", user = savedUser });
    }


}