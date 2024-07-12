using HotelierProject.DataAccessLayer.Abstract;
using HotelierProject.DataAccessLayer.Concrete;
using HotelierProject.DataAccessLayer.Repositories;
using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.DataAccessLayer.EntityFramework
{
    public class EfRoomDal : GenericRepository<Room>, IRoomDal
    {//GenericRepository'den miras al, Room için çalış ve IRoomDal Interface'inden de miras al.
        public EfRoomDal(Context context) : base(context)
        {
            //GenericRepository class'ında Context'i Constructor metot ile geçmiştik, o yüzden burada da geçmeliyiz, yoksa hata verir.

            //": base(context)" ifadesi, bir alt sınıfın üst sınıfın yapıcı yöntemini çağırdığını belirtmek için kullanılan bir ifadedir. Bu ifade alınan üst sınıfın yapıcı yöntemini çağırmak için kullanılır ve alt sınıfın yapıcı yöntemi içinde en üstte yer alır. Yani, "public EfRoomDal(Context context) : base(context)" ifadesi, EfRoomDal sınıfının yapıcı yöntemini tanımlar ve üst sınıf olan "base" sınıfının yapıcı yöntemini çağırır. Bu durumda, "context" parametresi, üst sınıfın yapıcı yöntemine aktarılır. Bu kullanım, genellikle bir alt sınıfın, üst sınıfın yapıcı yöntemine ihtiyaç duyduğu durumlarda kullanılır. Böylece alt sınıf, üst sınıfın yapıcı yöntemini çağırarak, üst sınıfın özelliklerini ve davranışını devralabilir ve gerekli başlatma işlemlerini gerçekleştirebilir.
        }

        public int RoomCount()//CRUD'a ek bir metot gerekirse burada yazıyoruz. Örnek RoomCount gibi.
        {
            var context = new Context();
            var value = context.Rooms.Count();
            return value;
        }
    }
}

