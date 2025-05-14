using AutoMapper;
using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Service.Services
{
    public class GlassesService : IGlassesService
    {
        private readonly IGlassesRepository _glassesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GlassesService(IUserRepository userRepository, IGlassesRepository glassesRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _glassesRepository = glassesRepository;
            _mapper = mapper;
        }

        public async Task<GlassesResponseDto> DeleteAsync(RemoveGlassesRequestDto removeGlassesRequestDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(removeGlassesRequestDto.Email , cancellationToken);
            if (user != null)
            {
                var glasses = await _glassesRepository.FindById(removeGlassesRequestDto.GlassesId, cancellationToken);
                if (glasses != null)
                {
                    user.Glasses.Clear();
                    await _glassesRepository.RemoveAsync(glasses, cancellationToken);
                    return new GlassesResponseDto
                    {
                        IsSuccess = true,
                        Message = "glasses successfully deleted"
                    };
                }
                return new GlassesResponseDto
                {
                    IsSuccess = false,
                    Message = "glasses not found!"
                };
            }
            return new GlassesResponseDto
            {
                IsSuccess = false,
                Message = "user not found!"
            };
		}

        public async Task<PagedResult<Glasses>> GetAllGlassesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
			return await _glassesRepository.FindAllAsync(pageNumber, pageSize, cancellationToken);
		}

        public async Task<GlassesResponseDto> SaveAsync(AddGlassesRequestDto addGlassesRequestDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(addGlassesRequestDto.Email, cancellationToken);
            if(user != null)
            {
                Glasses glasses = _mapper.Map<Glasses>(addGlassesRequestDto);
                glasses.Users.Add(user);
                var savedGlasses = await _glassesRepository.AddAsync(glasses, cancellationToken);
                //user.Glasses.Add(savedGlasses);
                //await _userRepository.UpdateAsync(user, cancellationToken);

                return new GlassesResponseDto
                {
                    IsSuccess = true,
                    Message = "glaasses successfully added"
                };
            }
            return new GlassesResponseDto
            {
                IsSuccess = false,
                Message = "user not found"
            };
        }

        public async Task<PagedResult<Glasses>> GetGlassesSuitableFaceTypeAsync(string faceType, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _glassesRepository.FindByFaceTypeAsync(faceType, pageNumber, pageSize, cancellationToken);
        }

        public async Task<bool> FavouriteGlassesAsync(long id, CancellationToken cancellationToken)
        {
            return await _glassesRepository.FavouriteGlassesAsync(id, cancellationToken);
        }

		public async Task<bool> RemoveFavouriteGlassesAsync(long id, CancellationToken cancellationToken)
		{
			return await _glassesRepository.RemoveFavouriteGlassesAsync(id, cancellationToken);
		}

        public async Task<bool> IncreaseViewAsync(long id, CancellationToken cancellationToken)
        {
            return await _glassesRepository.IncreaseViewAsync(id, cancellationToken);
        }

        public async Task<bool> SetActiveAsync(long id, bool isActive, string email, CancellationToken cancellationToken)
        {
            return await _glassesRepository.SetActiveAsync(id, isActive, email, cancellationToken);
        }

        public async Task<int> SetAllActiveAsync(bool isActive, string email, CancellationToken cancellationToken)
        {
            return await _glassesRepository.SetAllActiveAsync(isActive, email, cancellationToken);
        }
        public async Task<SellerStatisticsGlassesDto> GetMostViewedAsync(string email, CancellationToken cancellationToken)
        {
            var entity = await _glassesRepository.FindGlassesMostViewedAsync(email, cancellationToken);
            return _mapper.Map<SellerStatisticsGlassesDto>(entity);
        }
		public async Task<SellerStatisticsGlassesDto> GetMostLikedAsync(string email, CancellationToken cancellationToken)
		{
			var entity = await _glassesRepository.FindGlassesMostLikedAsync(email, cancellationToken);
			return _mapper.Map<SellerStatisticsGlassesDto>(entity);
		}
	}
}
