using HotelierProject.DataAccessLayer.Abstract;
using HotelierProject.DataAccessLayer.Concrete;
using HotelierProject.DataAccessLayer.Repositories;
using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.DataAccessLayer.EntityFramework
{
    public class EfContactDal : GenericRepository<Contact>, IContactDal
    {
        public EfContactDal(Context context) : base(context)
        {

        }

        public int GetContactCount()
        {
            var context = new Context();//Context sınıfımızdan bir örnek al.
            var value = context.Contacts.Count();//Context Sınıfındaki Contacts Entity'sindeki elemanların sayısını bize getir.
            return value;//ve bu değeri döndür.
        }
    }
}
