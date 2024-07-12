using System.ComponentModel.DataAnnotations;

namespace HotelierProject.WebUI.Dtos.LoginDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Kullanıcı adını giriniz!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifrenizi giriniz!")]
        public string Password { get; set; }
    }
}
