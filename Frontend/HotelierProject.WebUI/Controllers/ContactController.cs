using HotelierProject.WebUI.Dtos.ContactDto;
using HotelierProject.WebUI.Dtos.MessageCategoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace HotelierProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//Bir HTTP istemcisi oluşturuyoruz.
            var responseMessage = await client.GetAsync("http://localhost:5160/api/MessageCategory");//GET isteği yapılır. Elde edilen yanıt, responseMessage değişkenine atanır.
            var jsonData = await responseMessage.Content.ReadAsStringAsync();//HTTP yanıtının içeriğini okuyarak, JSON verisini bir string olarak alır ve jsonData'ya atar.
            var values = JsonConvert.DeserializeObject<List<ResultMessageCategoryDto>>(jsonData);//JSON verisini, ResultMessageCategoryDto türündeki nesneler listesine dönüştürür. Bu nesneler, values değişkenine atanır. Bu adım genellikle JSON verisini C# nesnelerine çevirmek için kullanılır.
            List<SelectListItem> selectList = (from x in values select new SelectListItem { Text = x.MessageCategoryName, Value = x.MessageCategoryID.ToString() }).ToList();
            //values listesindeki öğelerden SelectListItem türünde bir liste oluşturulur. Her öğe, MessageCategoryName özelliğini Text özelliğine, ve MessageCategoryID özelliğini Value özelliğine atanarak oluşturulur.
            ViewBag.selectList = selectList;//Oluşturulan selectList listesi, ViewBag nesnesinin selectList adlı dinamik özelliğine atanır.
            return View();//Index metodu, bir view'a döner.
        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateContactDto createContactDto)
        {
            createContactDto.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("http://localhost:5160/api/Contact", stringContent);
            return RedirectToAction("Index", "Default");
        }
    }
}
