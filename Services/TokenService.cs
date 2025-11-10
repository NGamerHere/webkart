using MyAdvancedApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAdvancedApi.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration config)
    {
        _config = config;
        string key = _config["Jwt:Key"] ?? "new_password_123445!@#$";
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.email),
        };
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = creds
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
    
}