namespace HotelierProject.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class //IGenericDal bir T değeri alacak, ve bu T değeri mutlaka Class olacak.
    {
        void Insert(T t);//Ekleme
        void Delete(T t);//Silme
        void Update(T t);//Güncelleme
        List<T> GetList();//Listeleme
        T GetByID(int id);//ID'ye göre getirme.
    }
}
