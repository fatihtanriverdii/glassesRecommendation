
namespace glassesRecommendation.Core.DTOs.Requests
{
    public class AddGlassesRequestDto
    {
        public string Image { get; set; }
		public string GlassesType { get; set; }
		public string Link { get; set; }
		public bool Oval { get; set; }
		public bool Oblong { get; set; }
		public bool Heart { get; set; }
		public bool Round { get; set; }
		public bool Square { get; set; }
		public string Email { get; set; }
    }
}
