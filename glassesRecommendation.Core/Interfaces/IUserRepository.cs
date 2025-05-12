using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AuthDto> AddAsync(User user, CancellationToken cancellationToken);
        Task<string> UpdateAsync(User user, CancellationToken cancellationToken);
        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
		Task<PagedResult<Glasses>> FindAllGlassesAsync(int pageNumber, int pageSize, string email, CancellationToken cancellationToken);
        Task<SellerStatisticsDto> GetSellerStatisticsAsync(string email);

	}
}
