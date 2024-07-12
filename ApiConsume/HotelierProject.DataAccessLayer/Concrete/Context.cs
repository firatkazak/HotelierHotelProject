using HotelierProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelierProject.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {//IdentityDbContext sınıfından miras aldırıyoruz Context özelliklerini kazandırmak için.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//OnConfiguring metodu SQL ile bağlantı yapmamızı sağlıyor. DbContextOptionsBuilder'dan optionsBuilder nesnesi otomatik geliyor.
            optionsBuilder.UseSqlServer("server=FIRAT\\SQLEXPRESS;initial catalog=ApiDb;integrated security=true;TrustServerCertificate=True;");
            //server: sunucunun adı. initial catalog: db'nin adı,
            //integrated security true/sspi: SQL Server’in Windows Authentication modunu desteklemesi ve işletim sisteminde kayıtlı user’in SQL Serverada kayıtlı olması gerekmektedir.
            //integrated security false: SQL Server Authentication ile bağlantının yapılmasıdır ve Connection String cümlesi içerisine UserID-Password özellikleri ve değerleri girilmelidir.
            //TrustServerCertificate true: SQL sunucusunun sertifikasını doğrulamaz ve bağlantıyı kabul eder.
            //TrustServerCertificate false: SQL sunucusu ile yapılan bağlantıda sunucu sertifikası doğrulanır. Eğer sunucunun sertifikası doğrulanmazsa veya güvenilir değilse, bağlantı hatalı olacak ve istemci tarafından kabul edilmeyecektir.
        }
        //DbSet türünde Property oluşturuyoruz, <> içine hangi Entity'i Sql'e aktaracaksak onu koyuyoruz. Çoğul bir isim veriyoruz.
        //Package Manager Console'u açıp add-migration migrationAdi şeklinde migration'ı basıyoruz.
        //Gelen Migration'a bakıyoruz, bir sorun yoksa;
        //update-database deyip migration'ı onaylıyoruz ve Database oluşmuş oluyor ya da yeni ekleyeceğimiz tablo ekleniyor.
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SendMessage> SendMessages { get; set; }
        public DbSet<MessageCategory> MessageCategories { get; set; }
        public DbSet<WorkLocation> WorkLocations { get; set; }
    }
}
