using AutoMapper;
using Domain.Entities;
using RestaurantLayer.Dtos.MenuCategory.Responses;

namespace RestaurantLayer.Mapper
{
    public class GetMenuCategoryResponseModelMap : Profile
    {
        public GetMenuCategoryResponseModelMap()
        {
            CreateMap<GetMenuCategoryResponseModel, MenuCategory>()
                .ForMember(dest => dest.Id, otp => otp.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, otp => otp.MapFrom(src => src.Name))
                .ForMember(dest => dest.ParentId, otp => otp.MapFrom(src => src.ParentId));
        }
    }
}
