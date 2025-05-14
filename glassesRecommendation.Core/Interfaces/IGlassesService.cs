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
        Task<bool> FavouriteGlassesAsync(long id, CancellationToken cancellationToken);
        Task<bool> RemoveFavouriteGlassesAsync(long id, CancellationToken cancellationToken);
        Task<bool> IncreaseViewAsync(long id, CancellationToken cancellationToken);
        Task<bool> SetActiveAsync(long id, bool isActive, string email, CancellationToken cancellationToken);
        Task<int> SetAllActiveAsync(bool isActive, string email, CancellationToken cancellationToken);
        Task<SellerStatisticsGlassesDto> GetMostViewedAsync(string email, CancellationToken cancellationToken);
        Task<SellerStatisticsGlassesDto> GetMostLikedAsync(string email, CancellationToken cancellationToken);
	}
}
