using AutoMapper;
using HotelierProject.EntityLayer.Concrete;
using HotelierProject.WebUI.Dtos.AboutDto;
using HotelierProject.WebUI.Dtos.AppUserDto;
using HotelierProject.WebUI.Dtos.BookingDto;
using HotelierProject.WebUI.Dtos.GuestDto;
using HotelierProject.WebUI.Dtos.LoginDto;
using HotelierProject.WebUI.Dtos.RegisterDto;
using HotelierProject.WebUI.Dtos.ServiceDto;
using HotelierProject.WebUI.Dtos.StaffDto;
using HotelierProject.WebUI.Dtos.SubscribeDto;
using HotelierProject.WebUI.Dtos.TestimonialDto;

namespace HotelierProject.WebUI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ResultServiceDto, Service>().ReverseMap();
            CreateMap<UpdateServiceDto, Service>().ReverseMap();
            CreateMap<CreateServiceDto, Service>().ReverseMap();

            CreateMap<CreateNewUserDto, AppUser>().ReverseMap();
            CreateMap<LoginUserDto, AppUser>().ReverseMap();

            CreateMap<ResultAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();

            CreateMap<ResultTestimonialDto, Testimonial>().ReverseMap();

            CreateMap<ResultStaffDto, Staff>().ReverseMap();

            CreateMap<CreateSubscribeDto, Subscribe>().ReverseMap();

            CreateMap<CreateBookingDto, Booking>().ReverseMap();
            CreateMap<ApprovedReservationDto, Booking>().ReverseMap();

            CreateMap<CreateGuestDto, Guest>().ReverseMap();
            CreateMap<UpdateGuestDto, Guest>().ReverseMap();

            CreateMap<ResultAppUserDto, AppUser>().ReverseMap();
        }
    }
}
