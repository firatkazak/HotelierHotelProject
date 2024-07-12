using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.DataAccessLayer.Abstract
{
    public interface IRoomDal : IGenericDal<Room>//IGenericDal'dan miras alacak ve Room sınıfı için çalışacak.
    {
        int RoomCount();//IGenericDal'daki CRUD işlemlerine ilaveten bir Metot gerekiyorsa onu buraya yazıyoruz.
    }
}
