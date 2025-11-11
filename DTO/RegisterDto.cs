using System.ComponentModel.DataAnnotations;

namespace WebCart.DTO;

public class RegisterDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string email { get; set; }

    [Required]
    [Phone]
    public required string phone { get; set; }

    [Required]
    public required string Password { get; set; }

    public int Age { get; set; }
}