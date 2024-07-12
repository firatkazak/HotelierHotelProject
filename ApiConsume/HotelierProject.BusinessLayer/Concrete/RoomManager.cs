using HotelierProject.BusinessLayer.Abstract;
using HotelierProject.DataAccessLayer.Abstract;
using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.BusinessLayer.Concrete
{
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomDal;//IRoomDAL'dan 1 örnek aldık.

        public RoomManager(IRoomDal roomDal)
        {
            _roomDal = roomDal;//Örneği Constructor'da geçtik.
        }

        public void TInsert(Room t)//Room tipindeki örneği
        {
            _roomDal.Insert(t);//Ekle
        }

        public void TDelete(Room t)//Room tipindeki örneği
        {
            _roomDal.Delete(t);//Sil
        }

        public void TUpdate(Room t)//Room tipindeki örneği
        {
            _roomDal.Update(t);//Güncelle
        }

        public List<Room> TGetList()
        {
            return _roomDal.GetList();////Room tipindeki verileri getir.
        }

        public Room TGetByID(int id)//Bir ID ver.
        {
            return _roomDal.GetByID(id);//Room tipindeki verileri verilen ID'ye göre getir.
        }

        public int TRoomCount()//Room Entity'sine özgü metot.
        {
            return _roomDal.RoomCount();//Oda sayısını getir.
        }
    }
}
