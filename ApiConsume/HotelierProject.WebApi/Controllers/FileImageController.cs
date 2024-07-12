using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileImageController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)//Bu metot, dosya yüklemek için kullanılır.
    {
        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //Yüklenen dosyanın adını belirlemek için, dosya adına rastgele bir GUID eklenir ve dosyanın orijinal uzantısı da korunarak fileName adlı bir değişkene atanır.
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Images/" + fileName);//Yüklenen dosyanın kaydedileceği yol belirlenir.
        //Directory.GetCurrentDirectory() mevcut çalışma dizinini döndürür ve ardından "Images/" dizini ve oluşturulan dosya adı ile birleştirilir.
        FileStream stream = new FileStream(path, FileMode.Create);//Belirlenen yol üzerinde bir FileStream oluşturulur.
        //Bu, dosyanın disk üzerinde oluşturulmasını ve yazılmasını sağlar.
        await file.CopyToAsync(stream);// İlgili dosya, oluşturulan FileStream üzerine asenkron olarak kopyalanır. Bu, dosyanın kaydedilmesini sağlar.
        return Created("", file);//Dosya başarıyla yüklendikten sonra, HTTP 201 Created durumunu ve yüklenen dosyayı içeren bir IActionResult nesnesi döndürülür.
    }
}

