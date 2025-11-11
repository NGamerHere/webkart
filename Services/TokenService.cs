using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCart.Models;

namespace WebCart.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration config)
    {
        _config = config;
        string key = _config["Jwt_Key"] ?? "new_password_123445!@#$";
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            Issuer = _config["Jwt_Issuer"],
            Audience = _config["Jwt_Audience"],
            SigningCredentials = creds
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
    
}