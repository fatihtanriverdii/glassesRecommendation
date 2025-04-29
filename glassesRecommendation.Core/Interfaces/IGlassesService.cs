using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IGlassesService
    {
        Task<GlassesResponseDto> SaveAsync(AddGlassesRequestDto addGlassesRequestDto, CancellationToken cancellationToken);
        Task<GlassesResponseDto> DeleteAsync(RemoveGlassesRequestDto removeGlassesRequestDto, CancellationToken cancellationToken);
        Task<PagedResult<Glasses>> GetAllGlassesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PagedResult<Glasses>> GetGlassesSuitableFaceTypeAsync(string faceType, int pageNumber, int pageSize, CancellationToken cancellationToken);

	}
}
