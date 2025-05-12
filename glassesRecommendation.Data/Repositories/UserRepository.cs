using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
using glassesRecommendation.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace glassesRecommendation.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<AuthDto> AddAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                return new AuthDto
                {
                    IsSuccess = true,
                    Message = "user successfully added"
                };
            }
            catch (Exception ex)
            {
                return new AuthDto
                {
                    IsSuccess = false,
                    Message = $"an error while adding user: {ex.Message}"
                };
            }
        }

		public async Task<PagedResult<Glasses>> FindAllGlassesAsync(int pageNumber, int pageSize, string email, CancellationToken cancellationToken)
		{
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

                if (user != null)
                {
                    var query = _context.Glasses
                        .Where(g => g.Users.Any(u => u.Email == email));

                    var totalCount = await query.CountAsync();

                    var items = await query
                        .OrderBy(g => g.Id)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);

                    return new PagedResult<Glasses>
                    {
                        Items = items,
                        TotalCount = totalCount,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    };
                }
                else
                {
					return new PagedResult<Glasses>
					{
						Items = new List<Glasses>(),
						PageNumber = pageNumber,
						PageSize = pageSize,
						TotalCount = 0
					};
				}
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while user's glasses getting{ex.Message}");
            }

		}

		public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while getting user: {ex.Message}");
            }
        }

        public async Task<string> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
                return "user successfully updated";
            } catch (Exception ex) {
                throw new Exception($"an error while updating user: {ex}");
            }
        }

        public async Task<SellerStatisticsDto> GetSellerStatisticsAsync(string email)
        {
            try
            {
                var glassesQuery = _context.Glasses
                                        .Where(g => g.Users.Any(u => u.Email == email));

                var totalGlasses = await glassesQuery.CountAsync();
                var activeGlasses = await glassesQuery.CountAsync(g => g.IsActive);
                var totalViews = await glassesQuery.SumAsync(g => g.Views);
                var totalLikes = await glassesQuery.SumAsync(g => g.Likes);

                return new SellerStatisticsDto
                {
                    TotalGlasses = totalGlasses,
                    ActiveGlasses = activeGlasses,
                    TotalViews = totalViews,
                    TotalLikes = totalLikes
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while getting statistics: {ex.Message}");
            }
        }
    }
}
