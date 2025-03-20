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

		public async Task<GlassesResponseDto> FindAllGlassesAsync(string email, CancellationToken cancellationToken)
		{
            try
            {
                var user = await _context.Users
                    .Include(u => u.Glasses)
                    .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

                if (user == null)
                {
                    return new GlassesResponseDto
                    {
                        IsSuccess = false,
                        Message = "user not found!"
                    };
                }

                return new GlassesResponseDto
                {
                    IsSuccess = true,
                    glasses = user.Glasses
                };
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
                throw new Exception($"an error while updating user: {ex.Message}");
            }
        }
    }
}
