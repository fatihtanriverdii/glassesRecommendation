using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IGlassesRepository
    {
        Task<Glasses> AddAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<GlassesResponseDto> RemoveAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<GlassesResponseDto> UpdateAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<List<Glasses>?> FindAllAsync(CancellationToken cancellationToken);
    }
}
