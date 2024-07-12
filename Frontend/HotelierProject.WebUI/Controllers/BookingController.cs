using HotelierProject.WebUI.Dtos.BookingDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelierProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult AddBooking()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBookingDto createBookingDto)//Yeni Rezervasyon oluşturacağımız için.
        {
            createBookingDto.Status = "Onay Bekliyor";//Rezervasyon yapıldığı anda Onay Bekliyor'a düşecek.
            createBookingDto.Description = string.Empty;//
            var client = _httpClientFactory.CreateClient();//Buradan sonrası aynı. StaffController'a bak.
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("http://localhost:5160/api/Booking", stringContent);
            return RedirectToAction("Index", "Default");
        }
    }
}
