
using System.ComponentModel.DataAnnotations;

namespace glassesRecommendation.Core.DTOs.Requests
{
    public class GetGlassesRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
