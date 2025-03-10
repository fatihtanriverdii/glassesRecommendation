using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IGlassesService
    {
        Task<GlassesResponseDto> SaveAsync(AddGlassesRequestDto addGlassesRequestDto, CancellationToken cancellationToken);
        Task<GlassesResponseDto> DeleteAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<List<Glasses>> GetAllGlasses(CancellationToken cancellationToken);
    }
}
