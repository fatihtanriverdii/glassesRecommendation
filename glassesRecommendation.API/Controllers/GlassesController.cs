using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.Interfaces;
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

		[HttpPatch("{id}/favorite")]
		public async Task<IActionResult> FavouriteGlass(long id, CancellationToken cancellationToken)
		{
			var success = await _glassesService.FavouriteGlassesAsync(id, cancellationToken);
			if (!success)
				return NotFound();
			return NoContent();
		}

		[HttpPatch("{id}/remove/favorite")]
		public async Task<IActionResult> RemoveFavouriteGlass(long id, CancellationToken cancellationToken)
		{
			var success = await _glassesService.RemoveFavouriteGlassesAsync(id, cancellationToken);
			if (!success)
				return NotFound();
			return NoContent();
		}

		[HttpPatch("{id}/increase/view")]
		public async Task<IActionResult> IncreaseView(long id, CancellationToken cancellationToken)
		{
			var success = await _glassesService.IncreaseViewAsync(id, cancellationToken);
			if (!success)
				return NotFound();
			return NoContent();
		}

		[HttpPatch("{id}/active")]
		public async Task<IActionResult> SetActive(long id, [FromBody] UpdateActiveDto updateActiveDto, [FromQuery] string email, CancellationToken cancellationToken)
		{
			var success = await _glassesService.SetActiveAsync(id, updateActiveDto.IActive, email, cancellationToken);
			if (!success)
				return NotFound();
			return NoContent();
		}

		[HttpPatch("active")]
		public async Task<IActionResult> SetAllActive([FromBody] UpdateActiveDto updateActiveDto, [FromQuery] string email, CancellationToken cancellationToken)
		{
			await _glassesService.SetAllActiveAsync(updateActiveDto.IActive, email, cancellationToken);
			return NoContent();
		}
	}
}
