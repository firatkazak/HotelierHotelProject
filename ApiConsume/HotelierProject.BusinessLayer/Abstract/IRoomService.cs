using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.BusinessLayer.Abstract
{
    public interface IRoomService : IGenericService<Room>
    {
        int TRoomCount();
    }
}
