namespace glassesRecommendation.Core.DTOs.Responses
{
    public class ProductScrapeResult
    {
        public string? Title { get; set; }
        public string? Price { get; set; }
        public List<string>? Description { get; set; }
    }
}
