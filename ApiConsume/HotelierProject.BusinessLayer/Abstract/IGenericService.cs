namespace HotelierProject.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class//Bir T değeri alacak ve bu T değeri bir Class olacak.
    {
        //GenericDal'daki metotları olduğu gibi buraya ekliyoruz ve sadece başlarına T ekliyoruz.
        //Sebebi: Manager tarafında çağırırken hem Business'taki isim hem de DataAccess'teki isim çakışmasın diye başına T koyduk.
        void TInsert(T t);
        void TDelete(T t);
        void TUpdate(T t);
        List<T> TGetList();
        T TGetByID(int id);
    }
}
