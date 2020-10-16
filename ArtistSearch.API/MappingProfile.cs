using ArtistSearch.API.Dtos;
using ArtistSearch.Infrastructure.Models;
using AutoMapper;


namespace ArtistSearch.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            DtoToDataModel();
            DataModelToDto();
        }

        private void DtoToDataModel()
        {

        }

        private void DataModelToDto()
        {
            CreateMap<ArtistModel, SearchResultDto>()
                .ForMember(src => src.ArtistId, opt => opt.MapFrom(dest => dest.Id))
                       .ForMember(src => src.ArtistName, opt => opt.MapFrom(dest => dest.Name));
        }
    }
}
