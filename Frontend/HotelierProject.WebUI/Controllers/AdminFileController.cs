using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HotelierProject.WebUI.Controllers;

public class AdminFileController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file)//Bu parametre, HTTP isteği sırasında gelen dosyayı temsil eder.
    {
        MemoryStream stream = new MemoryStream();//Bu, dosyanın içeriğini geçici olarak depolamak için kullanılacak bir bellek akışıdır.
        await file.CopyToAsync(stream);//file adlı IFormFile nesnesinin içeriği, stream adlı MemoryStream'e asenkron olarak kopyalanır.
        byte[] bytes = stream.ToArray();//MemoryStream içeriği bir byte dizisine dönüştürülür. Bu, dosyanın bayt dizisini temsil eder.
        ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);//Bayt dizisi, ByteArrayContent nesnesine atanır. Bu, HTTP isteği için içerik gövdesini temsil eder.
        byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);//İçerik türü başlığı, file adlı IFormFile nesnesinden alınan içerik türüne ayarlanır.
        MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();//Bu, çoklu parça form verisi içeren HTTP isteği için içerik gövdesini temsil eder.
        multipartFormDataContent.Add(byteArrayContent, "file", file.FileName);//Dosya içeriği ve adı, MultipartFormDataContent'e eklenir. Bu, HTTP isteğinin içeriğini oluşturur.
        HttpClient httpClient = new HttpClient();//Yeni bir HttpClient nesnesi oluşturulur. Bu, HTTP isteğini yapmak için kullanılacaktır.
        await httpClient.PostAsync("http://localhost:5160/api/FileProcess", multipartFormDataContent);//httpClient nesnesi kullanılarak, oluşturulan HTTP isteği gönderilir. 
        return View();//Metot, bir görünümü temsil eden bir ViewResult döndürür.
    }
}

