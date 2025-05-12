using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IGlassesRepository
    {
        Task<Glasses> AddAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<GlassesResponseDto> RemoveAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<GlassesResponseDto> UpdateAsync(Glasses glasses, CancellationToken cancellationToken);
        Task<PagedResult<Glasses>> FindAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Glasses?> FindById(long id, CancellationToken cancellationToken);
        Task<PagedResult<Glasses>> FindByFaceTypeAsync(string faceType, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> FavouriteGlassesAsync(long id, CancellationToken cancellationToken);
        Task<bool> RemoveFavouriteGlassesAsync(long id, CancellationToken cancellationToken);
        Task<bool> IncreaseViewAsync(long id, CancellationToken cancellationToken);
        Task<bool> SetActiveAsync(long id, bool isActive, string email, CancellationToken cancellationToken);
        Task<int> SetAllActiveAsync(bool isActive, string email, CancellationToken cancellationToken);
	}
}
