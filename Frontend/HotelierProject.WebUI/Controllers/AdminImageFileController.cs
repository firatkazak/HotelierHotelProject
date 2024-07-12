using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HotelierProject.WebUI.Controllers;

public class AdminImageFileController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file)
    {
        MemoryStream stream = new MemoryStream();//Bellekte bir MemoryStream oluşturuyoruz.
        await file.CopyToAsync(stream);//Gelen dosyayı asenkron olarak MemoryStream'e kopyalıyoruz.
        byte[] bytes = stream.ToArray();//MemoryStream'i byte dizisine dönüştürüyoruz.
        ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);//Byte dizisini içeren bir ByteArrayContent oluşturuyoruz.
        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);//ByteArrayContent'in içeriğinin türünü ve dosyanın türünü belirtiyoruz.
        MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();//Multipart form verisi içeren bir MultipartFormDataContent oluşturuyoruz.
        multipartFormDataContent.Add(byteArrayContent, "file", file.FileName);//ByteArrayContent'i MultipartFormDataContent'e ekliyoruz.
        //NOT: "file" parametresi, sunucuda beklenen dosya adını temsil eder.
        HttpClient httpClient = new HttpClient();//HTTP istekleri göndermek için bir HttpClient oluşturuyoruz.
        await httpClient.PostAsync("http://localhost:5160/api/FileImage", multipartFormDataContent);//Sunucuya asenkron olarak POST isteği gönderiyoruz.
        return View();//İşlem tamamlandıktan sonra bir view döndürüyoruz.
    }
}
