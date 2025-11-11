using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WebCart.DTO;
using WebCart.Models;
using WebCart.Services;

namespace WebCart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;
    private readonly UserService _userService;

    public CompanyController(CompanyService companyService, UserService userService)
    {
        _companyService = companyService;
        _userService = userService;
    }

    [HttpGet]
    [Authorize("ADMIN")]
    public async Task<IActionResult> Get()
    {
        var companies = await _companyService.GetCompanies();
        return Ok(new
        { companies });
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        Console.WriteLine("the user role "+userRole);
        var company=await _companyService.GetCompany(id);
        return Ok(company);
    }

    [HttpPost("create")]
    [Authorize("AdminOrSeller")]
    public async Task<IActionResult> CreateCompany([FromBody] NewCompany newCompany)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdString, out var userId))
        {
            return Unauthorized(new { error = "Invalid user ID in token." });
        }
        Company company = new Company { 
            Name = newCompany.Name,
            GstNumber = newCompany.GstNumber,
            Address = newCompany.Address,
            UserId = userId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var savedCompany=await _companyService.AddCompany(company);
        return Ok(new
        {
            message = "new company saved successfully",
            savedCompany
        });
    }
    
    [HttpPut("{id}")]
    [Authorize("AdminOrSeller")]
    public async Task<IActionResult> EditCompany([FromBody] Company company, [FromQuery] int id)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        Console.WriteLine("the user role "+userRole);
        var newCompany=await _companyService.AddCompany(company);
        return Ok(new
        {
            message = "new company saved successfully",
            newCompany
        });
    }
    
}