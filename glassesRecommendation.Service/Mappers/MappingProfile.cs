using AutoMapper;
using glassesRecommendation.Core.DTOs.Requests;
using glassesRecommendation.Core.DTOs.Responses;
using glassesRecommendation.Core.Models;

namespace glassesRecommendation.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<AddGlassesRequestDto, Glasses>();
            CreateMap<Glasses, SellerStatisticsGlassesDto>();
        }
    }
}
