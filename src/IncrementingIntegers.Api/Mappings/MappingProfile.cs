using AutoMapper;
using IncrementingIntegers.Contract.V1.Responses;
using IncrementingIntegers.DataAccess.Entities;
using IncrementingIntegers.Logic.Results;

namespace IncrementingIntegers.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NextIdResult, NextIdResponse>();
            CreateMap<CurrentIdResult, CurrentIdResponse>();
            CreateMap<UniqueIntegerUserTableEntity, NextIdResult>()
                .ForMember(dest => dest.NextId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UniqueIntegerUserTableEntity, CurrentIdResult>()
                .ForMember(dest => dest.CurrentId, opt => opt.MapFrom(src => src.Id));
        }
    }
}