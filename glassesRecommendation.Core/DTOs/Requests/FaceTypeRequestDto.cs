using glassesRecommendation.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace glassesRecommendation.Core.DTOs.Requests
{
    public class FaceTypeRequestDto
    {
        [Required]
        public FaceType FaceType { get; set; }
    }
}