using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.BusinessLayer.Abstract
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        public List<AppUser> TUserListWithWorkLocation();
        public List<AppUser> TUsersListWithWorkLocations();
        int TAppUserCount();
    }
}
