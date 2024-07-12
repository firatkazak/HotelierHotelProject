using HotelierProject.DataAccessLayer.Concrete;
using HotelierProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelierProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserWorkLocationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            Context context = new Context();
            //Altta, .Include metoduna kadar, values'a atanan değer: İlişkisel veri yoluyla "Users" tablosuyla ilişkili olan "WorkLocation" tablosundan verileri de almasını sağlar. Bu şekilde, "Users" tablosundaki her kullanıcının çalışma konumuna ait verileri de alırız.
            var values = context.Users.Include(u => u.WorkLocation).Select(y => new AppUserWorkLocationViewModel
            {
                Name = y.Name,
                Surname = y.Surname,
                WorkLocationID = y.WorkLocationID,
                WorkLocationName = y.WorkLocation.WorkLocationName,
                City = y.City,
                Country = y.Country,
                Gender = y.Gender,
                ImageUrl = y.ImageUrl,
            }).ToList();
            //Üsttte, .Select metoduna kadar, values'a atanan değer: Bu bölüm, alınan verileri belirli bir ViewModel'e dönüştürür. ViewModel, genellikle bir görünümde gösterilmek üzere özel olarak oluşturulan bir model sınıfıdır. Burada, "AppUserWorkLocationViewModel" adlı bir ViewModel oluşturuluyor ve veriler bu ViewModel'e dönüştürülüyor. Daha sonra, .ToList() ile bu veriler liste halinde alınıyor.
            return Ok(values);//Bu, dönüştürülmüş verileri 200 OK durum kodu ile birlikte HTTP yanıtı olarak döndürür. "Ok" ifadesi, işlemin başarılı olduğunu ve yanıtın veri içerdiğini belirtmek için kullanılır.
        }
    }
}
