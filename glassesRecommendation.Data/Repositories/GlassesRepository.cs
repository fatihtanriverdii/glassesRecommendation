using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
using glassesRecommendation.Data.Context;
using Microsoft.EntityFrameworkCore;

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
    }
}
