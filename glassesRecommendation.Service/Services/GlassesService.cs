using AutoMapper;
using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<GlassesResponseDto> DeleteAsync(Glasses glasses, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Glasses>> GetAllGlasses(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<GlassesResponseDto> SaveAsync(AddGlassesRequestDto addGlassesRequestDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(addGlassesRequestDto.Email, cancellationToken);
            if(user != null)
            {
                Glasses glasses = _mapper.Map<Glasses>(addGlassesRequestDto);
                glasses.Users.Add(user);
                var savedGlasses = await _glassesRepository.AddAsync(glasses, cancellationToken);
                user.Glasses.Add(savedGlasses);
                await _userRepository.UpdateAsync(user, cancellationToken);

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
    }
}
