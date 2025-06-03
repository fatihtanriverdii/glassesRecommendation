using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
using glassesRecommendation.Core.Enums;
using glassesRecommendation.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace glassesRecommendation.Data.Repositories
{
    public class GlassesRepository : IGlassesRepository
    {
        private readonly AppDbContext _context;

        public GlassesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Glasses> AddAsync(Glasses glasses, CancellationToken cancellationToken)
        {
            try
            {
                _context.Glasses.Add(glasses);
                await _context.SaveChangesAsync(cancellationToken);

                // the entity's Id will be set after SaveChangesAsync, so simply
                // return the same instance instead of querying by the number of
                // affected rows
                return glasses;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while glasses adding: {ex.Message}");
            }
        }

        public async Task<PagedResult<Glasses>> FindAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Glasses> query = _context.Glasses.AsQueryable();

            try
            {
                int totalCount = await query.CountAsync(cancellationToken);

                List<Glasses> items = await query
                    .Where(g => g.IsActive == true)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                return new PagedResult<Glasses>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while getting glasses: {ex.Message}");
            }
        }

        public async Task<Glasses?> FindById(long id, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Glasses.FirstOrDefaultAsync(g => g.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while getting glasses: {ex.Message}");
            }
        }

        public async Task<GlassesResponseDto> RemoveAsync(Glasses glasses, CancellationToken cancellationToken)
        {
            try
            {
                _context.Glasses.Remove(glasses);
                await _context.SaveChangesAsync(cancellationToken);
                return new GlassesResponseDto
                {
                    IsSuccess = true,
                    Message = "glasses successfully removed"
                };
            }
            catch (Exception ex)
            {
                return new GlassesResponseDto
                {
                    IsSuccess = false,
                    Message = $"an error while removing glasses: {ex.Message}"
                };
            }
        }

        public async Task<GlassesResponseDto> UpdateAsync(Glasses glasses, CancellationToken cancellationToken)
        {
            try
            {
                _context.Glasses.Update(glasses);
                await _context.SaveChangesAsync(cancellationToken);
                return new GlassesResponseDto
                {
                    IsSuccess = true,
                    Message = "glasses successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new GlassesResponseDto
                {
                    IsSuccess = false,
                    Message = $"an error while updating glasses: {ex.Message}"
                };
            }
        }

        public async Task<PagedResult<Glasses>> FindByFaceTypeAsync(FaceType faceType, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Glasses> query = _context.Glasses.AsQueryable();

            try
            {
                switch (faceType)
                {
                    case FaceType.Oval:
                        query = query.Where(g => g.Oval);
                        break;
                    case FaceType.Oblong:
                        query = query.Where(g => g.Oblong);
                        break;
                    case FaceType.Heart:
                        query = query.Where(g => g.Heart);
                        break;
                    case FaceType.Round:
                        query = query.Where(g => g.Round);
                        break;
                    case FaceType.Square:
                        query = query.Where(g => g.Square);
                        break;
                    default:
                        query = query.Where(g => false);
                        break;
                }

                int totalCount = await query.CountAsync(cancellationToken);

                List<Glasses> items = await query
                    .Where(g => g.IsActive == true)
                    .OrderBy(x => Guid.NewGuid())
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                return new PagedResult<Glasses>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };

            }
            catch (Exception ex)
            {
                throw new Exception($"an error while getting glasses: {ex.Message}");
            }
        }

        public async Task<bool> FavouriteGlassesAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var glasses = await _context.Glasses
                                    .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
                if (glasses == null)
                    return false;

                glasses.Likes += 1;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while fav a glasses: {ex.Message}");
            }
        }

        public async Task<bool> RemoveFavouriteGlassesAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var glasses = await _context.Glasses
                                    .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

                if (glasses == null)
                    return false;

                glasses.Likes -= 1;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while remove fav glasses: {ex.Message}");
            }
        }

        public async Task<bool> IncreaseViewAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var glasses = await _context.Glasses
                                    .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

                if (glasses == null)
                    return false;

                glasses.Views += 1;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while increasing view: {ex.Message}");
            }
        }

        public async Task<bool> SetActiveAsync(long id, bool isActive, string email, CancellationToken cancellationToken)
        {
            try
            {
                var glasses = await _context.Glasses
                                    .Where(g => g.Users.Any(u => u.Email == email) 
                                        && g.Id == id)
                                    .FirstOrDefaultAsync(cancellationToken);

                if (glasses == null)
                    return false;

                glasses.IsActive = isActive;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while setting activity: {ex.Message}");
            }
        }

        public async Task<int> SetAllActiveAsync(bool isActive, string email, CancellationToken cancellationToken)
        {
            try
            {
                var glasses = await _context.Glasses
                                    .Where(g => g.Users.Any(u => u.Email == email))
                                    .ToListAsync (cancellationToken);

                foreach(var g in glasses)
                    g.IsActive = isActive;

                await _context.SaveChangesAsync(cancellationToken);
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while setting all activity: {ex.Message}");
            }
        }

        public async Task<Glasses> FindGlassesMostViewedAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Glasses
                            .Where(g => g.Users.Any(g => g.Email == email))
                            .OrderByDescending(g => g.Views)
                            .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while finding the most viewed glasses: {ex.Message}");
            }
        }

        public async Task<Glasses> FindGlassesMostLikedAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Glasses
							.Where(g => g.Users.Any(g => g.Email == email))
							.OrderByDescending(g => g.Likes)
                            .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while finding the most liked glasses: {ex.Message}");
            }
        }
    }
}
