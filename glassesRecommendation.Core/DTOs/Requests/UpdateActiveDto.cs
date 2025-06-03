using System.ComponentModel.DataAnnotations;

namespace glassesRecommendation.Core.DTOs.Requests
{
    public class UpdateActiveDto
    {
        [Required]
        public bool IActive { get; set; }
    }
}
