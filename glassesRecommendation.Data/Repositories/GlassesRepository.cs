using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
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
                long id = await _context.SaveChangesAsync(cancellationToken);
                return await _context.Glasses.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"an error while glasses adding: {ex.Message}");
            }
        }

        public async Task<List<Glasses>?> FindAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Glasses.ToListAsync(cancellationToken);
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
                return new GlassesResponseDto {
                    IsSuccess = true,
                    Message = "glasses successfully updated"
                };
            } catch (Exception ex) {
                return new GlassesResponseDto
                {
                    IsSuccess = false,
                    Message = $"an error while updating glasses: {ex.Message}"
                };
            }
        }

        public async Task<PagedResult<Glasses>> FindByFaceTypeAsync(string faceType, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Glasses> query = _context.Glasses.AsQueryable();

            try
            {
                switch (faceType)
                {
                    case "Oval":
                        query = query.Where(g => g.Oval == true);
                        break;
                    case "Oblong":
						query = query.Where(g => g.Oblong == true);
                        break;
					case "Heart":
						query = query.Where(g => g.Heart == true);
                        break;
					case "Round":
						query = query.Where(g => g.Round == true);
                        break;
					case "Square":
						query = query.Where(g => g.Square == true);
                        break;
					default:
						query = query.Where(g => false);
                        break;
				}

                int totalCount = await query.CountAsync(cancellationToken);

                List<Glasses> items = await query
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
    }
}
