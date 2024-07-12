using HotelierProject.EntityLayer.Concrete;
using HotelierProject.WebUI.Dtos.LoginDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        //SignInManager Identity kütüphanesi ile birlikte geliyor.
        //Login işlemi AppUser için yapılacağı için buradan bir örnek alıyoruz.
        //Bunun için aşağıda Constructor'ı geçiyoruz.
        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)//Valid ise;
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);
                //PasswordSignInAsync: Belirtilen userName ve password birleşiminde oturum açmaya çalışır.
                //1.false: isPersistent: Tarayıcı kapatıldıktan sonra oturum açma tanımlama bilgisinin kalıcı olup olmadığını belirtir.
                //2.false: lockoutOnFailure: Oturum açma başarısız olursa kullanıcı hesabının kilitlenmesi gerekip gerekmediğini belirtir.
                if (result.Succeeded)//Sonuç başarılı ise;
                {
                    return RedirectToAction("Index", "Staff");//Staff'ın Index'ine yönlendirecek.
                }
                else
                {
                    return View();//Login sayfasını tekrar döndürecek.
                }
            }
            return View();//Login sayfasını tekrar döndürecek.
        }
    }
}

