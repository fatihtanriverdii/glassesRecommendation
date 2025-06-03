
using System.ComponentModel.DataAnnotations;

namespace glassesRecommendation.Core.DTOs.Requests
{
    public class AddGlassesRequestDto
    {
        [Required]
        public string Image { get; set; }

        [Required]
        public string GlassesType { get; set; }

        [Required]
        [Url]
        public string Link { get; set; }

        public bool IsRecycling { get; set; }
        public bool Oval { get; set; }
        public bool Oblong { get; set; }
        public bool Heart { get; set; }
        public bool Round { get; set; }
        public bool Square { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
