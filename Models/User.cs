using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyAdvancedApi.Models
{
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

        public bool PasswordCheck(string inputPassword)
        {
            return inputPassword == Password;
        }

    }
}