using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.DTOs.Responses
{
    public class GlassesResponseDto
    {
        public bool IsSuccess { get; set; }
        public List<Glasses>? glasses { get; set; }
        public string? Message { get; set; }
    }
}
