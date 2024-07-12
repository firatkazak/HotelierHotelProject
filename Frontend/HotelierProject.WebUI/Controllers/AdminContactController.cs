using HotelierProject.WebUI.Dtos.ContactDto;
using HotelierProject.WebUI.Dtos.SendMessageDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelierProject.WebUI.Controllers;

public class AdminContactController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AdminContactController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Inbox()
    {

        var client1 = _httpClientFactory.CreateClient();//1 istemci oluşturduk.
        var responseMessage1 = await client1.GetAsync("http://localhost:5160/api/Contact");//Contact'a istek yaptık.

        var client2 = _httpClientFactory.CreateClient();//istemci.
        var responseMessage2 = await client2.GetAsync("http://localhost:5160/api/Contact/GetContactCount");//GetContactCount'a istek.

        var client3 = _httpClientFactory.CreateClient();//istemci.
        var responseMessage3 = await client3.GetAsync("http://localhost:5160/api/SendMessage/GetSendMessageCount");//GetSendMessageCount'a istek.

        if (responseMessage1.IsSuccessStatusCode)//Eğer Contact'a yapılan istek başarılı olursa;
        {
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<InboxContactDto>>(jsonData1);//Gelen kutusundaki mesajları Listeliyoruz.

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.contactCount = jsonData2;//Gelen Kutusundaki sayısı gösteriyor.

            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.sendMessageCount = jsonData3;//Giden Kutusundaki sayısı gösteriyor.

            return View(values);
        }
        return View();
    }

    public async Task<IActionResult> SendBox()
    {
        var client = _httpClientFactory.CreateClient();//1 istemci oluşturduk.
        var responseMessage = await client.GetAsync("http://localhost:5160/api/SendMessage");//SendMessage'e istek yaptık.
        if (responseMessage.IsSuccessStatusCode)//Eğer SendMessage'e yapılan istek başarılı olursa;
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultSendBoxDto>>(jsonData);//Giden kutusundaki mesajları Listeliyoruz.
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult AddSendMessage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSendMessage(CreateSendMessage createSendMessage)
    {
        createSendMessage.SenderMail = "admin@gmail.com";//Mesajı gönderenin Mail adresi.
        createSendMessage.SenderName = "admin";//Mesajı gönderen.
        createSendMessage.Date = DateTime.Parse(DateTime.Now.ToShortDateString());//Mesajın gönderilme tarihi.
        var client = _httpClientFactory.CreateClient();//İstemci oluşturduk. Gerisi aynı.
        var jsonData = JsonConvert.SerializeObject(createSendMessage);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5160/api/SendMessage", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("SendBox");
        }
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> MessageDetailsBySendBox(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5160/api/SendMessage/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<GetMessageByIDDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> MessageDetailsByInbox(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5160/api/Contact/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<InboxContactDto>(jsonData);
            return View(values);
        }
        return View();
    }

    public PartialViewResult SideBarAdminContactPartial()
    {
        return PartialView();
    }

    public PartialViewResult SideBarAdminContactCategoryPartial()
    {
        return PartialView();
    }
}
