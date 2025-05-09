using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckIsEmailExists(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(email, cancellationToken);
            if (user == null)
                return false;
            return true;
        }

		public async Task<PagedResult<Glasses>> GetAllGlassesAsync(int pageNumber, int pageSize, string email, CancellationToken cancellationToken)
		{
			var glassesResponse = await _userRepository.FindAllGlassesAsync(pageNumber, pageSize, email, cancellationToken);
            return glassesResponse;
		}

		public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(email, cancellationToken);
            return user;
        }

        public async Task<AuthDto> SaveAsync(User user, CancellationToken cancellationToken)
        {
            var authResponse = await _userRepository.AddAsync(user, cancellationToken);
            return authResponse;
        }
    }
}
