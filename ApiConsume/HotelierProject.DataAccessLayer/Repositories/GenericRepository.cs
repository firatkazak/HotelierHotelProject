using HotelierProject.DataAccessLayer.Abstract;
using HotelierProject.DataAccessLayer.Concrete;

namespace HotelierProject.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class//T değeri alacak, IGenericDal'dan Miras alacak ve T bir class olacak.
    {
        private readonly Context _context;//Context sınıfından 1 tane örnek aldık.

        public GenericRepository(Context context)
        {
            _context = context;//Constructor'ı oluşturduk.
        }

        public void Insert(T t)
        {
            _context.Add(t);//t değerini ekle.
            _context.SaveChanges();//değişiklikleri kaydet.
        }

        public void Delete(T t)
        {
            _context.Remove(t);//t değerini sil.
            _context.SaveChanges();//değişiklikleri kaydet.
        }

        public void Update(T t)
        {
            _context.Update(t);//t değerini güncelle.
            _context.SaveChanges();//değişiklikleri kaydet.
        }

        public List<T> GetList()
        {
            var value = _context.Set<T>().ToList();//Verilen Entity'deki Değerleri getir.
            return value;//Getirdiğin değerleri döndür.
        }

        public T GetByID(int id)
        {
            var value = _context.Set<T>().Find(id);//Entity'deki Değeri Id'sine göre getir.
            return value;//Değeri döndür.
        }

    }
}
