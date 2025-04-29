using glassesRecommendation.Core.DTOs.Responses;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IScraperService
    {
        Task<ProductScrapeResult> ScrapeProductAsync(string url, CancellationToken cancellationToken);
    }
}
