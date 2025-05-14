namespace glassesRecommendation.Core.DTOs.Responses
{
	public class SellerStatisticsGlassesDto
	{
		public string Image { get; set; }
		public string GlassesType { get; set; }
		public string Link { get; set; }
		public bool IsRecycling { get; set; }
		public bool IsActive { get; set; } = true;
		public long Likes { get; set; }
		public long Views { get; set; }
	}
}