using HotelierProject.BusinessLayer.Abstract;
using HotelierProject.DataAccessLayer.Abstract;
using HotelierProject.EntityLayer.Concrete;

namespace HotelierProject.BusinessLayer.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public void TDelete(About t)
        {
            _aboutDal.Delete(t);//t'den gelen değeri sil.
        }

        public About TGetByID(int id)
        {
            return _aboutDal.GetByID(id);//id'ye göre getir.
        }

        public List<About> TGetList()
        {
            return _aboutDal.GetList();//listele(AboutManager'dayız haliyle About'a göre olacak.)
        }

        public void TInsert(About t)
        {
            _aboutDal.Insert(t);//t'den gelen değeri ekle.
        }

        public void TUpdate(About t)
        {
            _aboutDal.Update(t);//t'den gelen değeri güncelle.
        }
    }
}
