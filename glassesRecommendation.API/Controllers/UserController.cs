using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace glassesRecommendation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGlassesService _glassesService;

        public UserController(IGlassesService glassesService)
        {
            _glassesService = glassesService;
        }

        [HttpPost("add/glasses")]
        public async Task<IActionResult> addGlasses(AddGlassesRequestDto addGlassesRequestDto, CancellationToken cancellationToken)
        {
            var response = await _glassesService.SaveAsync(addGlassesRequestDto, cancellationToken);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
