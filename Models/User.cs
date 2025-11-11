using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebCart.Enum;

namespace WebCart.Models
{
    [Index(nameof(email), IsUnique = true)]
    [Index(nameof(phone), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string email { get; set; }

        [Required]
        [Phone]
        public required string phone { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }

        public int Age { get; set; }
        
        public Role  Role { get; set; }
        
        public ICollection<Company> Companies { get; set; } = new List<Company>();

        public bool PasswordCheck(string inputPassword)
        {
            return inputPassword == Password;
        }

    }
}