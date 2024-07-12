using HotelierProject.EntityLayer.Concrete;
using HotelierProject.WebUI.Dtos.RegisterDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;//UserManager Identity kütüphanesi ile birlikte geliyor. Register AppUser için yapılacağından bir örnek alıyoruz.
        public RegisterController(UserManager<AppUser> userManager)//Bunun için aşağıda Constructor'ı geçiyoruz.
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateNewUserDto createNewUserDto)//Yeni kullanıcı kaydı için bir örnek aldık.
        {
            if (!ModelState.IsValid)//Valid değilse;
            {
                return View();//Kayıt sayfasını tekrar döndürecek.
            }
            AppUser appUser = new AppUser()//AppUser sınıfından yeni bir appUser(kullanıcı) oluştur.
            {
                Name = createNewUserDto.Name,//CreateNewUserDto'daki Name'i, AppUser'daki Name'e ata.
                Surname = createNewUserDto.Surname,
                UserName = createNewUserDto.Username,
                Email = createNewUserDto.Mail,
                City = createNewUserDto.City,
                Country = createNewUserDto.Country,
                Gender = createNewUserDto.Gender,
                ImageUrl = createNewUserDto.ImageUrl,
                WorkLocationID = createNewUserDto.WorkLocationID,
                WorkDepartment = createNewUserDto.WorkDepartment,//Buraya kadar benzer aynı işlemi tekrarladık.
            };
            //NOT: Identity sınıfında Password değeri AppUser nesnesinde değil, onun dışında tanımlanır!
            IdentityResult result = await _userManager.CreateAsync(appUser, createNewUserDto.Password);
            //burada _userManager'dan CreateAsync ile asenkron bir result yaratıyoruz.
            //1. parametre appUser nesnemiz, 2. parametre createNewUserDto'dan gelen Password değerimiz oluyor.
            if (result.Succeeded)//Eğer sonuç başarılıysa;
            {
                return RedirectToAction("Index", "Login");//Login'in Index'ine yönlendirecek.
            }
            return View();//Başarısız olursa aynı sayfayı yenileyecek.
        }
    }
}
