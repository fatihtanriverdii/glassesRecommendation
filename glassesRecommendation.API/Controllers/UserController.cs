using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace glassesRecommendation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class UserController : ControllerBase
    {
        private readonly IGlassesService _glassesService;
        private readonly IUserService _userService;

        public UserController(IGlassesService glassesService, IUserService userService)
        {
            _glassesService = glassesService;
            _userService = userService;
        }

        [HttpPost("add/glasses")]
        public async Task<IActionResult> addGlasses(AddGlassesRequestDto addGlassesRequestDto, CancellationToken cancellationToken)
        {
            var response = await _glassesService.SaveAsync(addGlassesRequestDto, cancellationToken);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("remove/glasses")]
        public async Task<IActionResult> removeGlasses(RemoveGlassesRequestDto removeGlassesRequestDto, CancellationToken cancellationToken)
        {
            var response = await _glassesService.DeleteAsync(removeGlassesRequestDto, cancellationToken);
            if (response.IsSuccess) 
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("glasses")]
        public async Task<IActionResult> getGlasses(
            [FromQuery] string email,
            CancellationToken cancellationToken,
		    [FromQuery] int pageNumber = 1,
			[FromQuery] int pageSize = 5)
        {
            var response = await _userService.GetAllGlassesAsync(pageNumber, pageSize, email, cancellationToken);
            return Ok(response);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetSellerStatistics(
            [FromQuery] string email,
            CancellationToken cancellationToken)
        {
            var response = await _userService.GetSellerStatsAsync(email);
            return Ok(response);
        }

        [HttpGet("most-viewed")]
        public async Task<IActionResult> GetMostViewed(
            [FromQuery] string email,
            CancellationToken cancellationToken)
        {
            var dto = await _glassesService.GetMostViewedAsync(email, cancellationToken);
            if(dto == null)
                return NoContent();
            return Ok(dto);
        }

		[HttpGet("most-liked")]
		public async Task<IActionResult> GetMostLiked(
	        [FromQuery] string email,
	        CancellationToken cancellationToken)
		{
			var dto = await _glassesService.GetMostLikedAsync(email, cancellationToken);
			if (dto == null)
				return NoContent();
			return Ok(dto);
		}
	}
}
