using AutoMapper;
using HotelierProject.DtoLayer.Dtos.RoomDto;
using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RoomAddDto, Room>().ReverseMap();
            CreateMap<UpdateRoomDto, Room>().ReverseMap();
        }
    }
}
