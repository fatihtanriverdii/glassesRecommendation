using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace glassesRecommendation.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GlassesController : ControllerBase
	{
		private readonly IGlassesService _glassesService;

		public GlassesController(IGlassesService glassesService) {
			_glassesService = glassesService;
		}

		[HttpGet("suitable/glasses")]
		public async Task<IActionResult> GetSuitableGlasses(
			[FromQuery] FaceTypeRequestDto faceTypeRequestDto,
			CancellationToken cancellationToken,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int pageSize = 5)
		{
			var pagedResult = await _glassesService.GetGlassesSuitableFaceTypeAsync(faceTypeRequestDto.FaceType, pageNumber, pageSize, cancellationToken);
			return Ok(pagedResult);
		}

		[HttpGet("glasses")]
		public async Task<IActionResult> GetAllGlassesAsync(
			CancellationToken cancellationToken,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int pageSize = 5)
		{
			var pagegResult = await _glassesService.GetAllGlassesAsync(pageNumber, pageSize, cancellationToken);
			return Ok(pagegResult);
		}
	}
}
