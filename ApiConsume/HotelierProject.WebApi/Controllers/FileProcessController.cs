using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileProcessController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {//FromForm: Frontend'deki Controller'ın View'ine file parametresini bağlayıp Dosya aktarımını sağlayan Attribute.
        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);//Dosyanın adını, rastgele bir GUID ile birleştirip benzersiz bir ad oluşturuyoruz.
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files/" + fileName);//Dosyanın kaydedileceği tam yolunu belirliyoruz.
        FileStream stream = new FileStream(path, FileMode.Create);//FileStream kullanarak belirtilen yol ve ad ile dosyayı oluşturuyoruz.
        await file.CopyToAsync(stream);//Gelen dosyayı oluşturduğumuz FileStream'e asenkron olarak kopyalıyoruz.
        return Created("", file);//Dosyanın başarıyla yüklendiğini belirten bir Created (201) HTTP yanıtı döndürüyoruz. Dosya bilgileri file nesnesi üzerinden alınabilir.
    }
}
