using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Core.Interfaces
{
    public interface IUserService
    {
        Task<AuthDto> SaveAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> CheckIsEmailExists(string email, CancellationToken cancellationToken);
		Task<PagedResult<Glasses>> GetAllGlassesAsync(int pageNumber, int pageSize, string email, CancellationToken cancellationToken);
    }
}
