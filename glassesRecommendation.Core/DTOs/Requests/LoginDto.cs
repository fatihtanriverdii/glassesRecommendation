using System.ComponentModel.DataAnnotations;

namespace glassesRecommendation.Core.DTOs.Requests
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
