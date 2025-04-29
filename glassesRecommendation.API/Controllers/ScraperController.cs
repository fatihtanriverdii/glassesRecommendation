using glassesRecommendation.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace glassesRecommendation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly IScraperService _scraperService;

        public ScraperController(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        [HttpGet("scrape")]
        public async Task<IActionResult> ScrapeProduct([FromQuery] string url, CancellationToken cancellationToken)
        {
            var result = await _scraperService.ScrapeProductAsync(url, cancellationToken);
            return Ok(result);
        }
    }
}