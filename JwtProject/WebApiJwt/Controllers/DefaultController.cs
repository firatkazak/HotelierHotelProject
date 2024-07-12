using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DefaultController : ControllerBase
{
    [HttpGet("[action]")]//Action'a gidecek. O yüzden [] içinde yazdık.
    public IActionResult TokenGenerator()
    {
        return Ok(new CreateToken().TokenCreate());
        //Models'te oluşturduğumuz CreateToken nesnesinin TokenCreate metodu ile bir tane Token yarattık.
    }

    [HttpGet("[action]")]
    public IActionResult AdminTokenGenerator()
    {
        return Ok(new CreateToken().TokenCreateAdmin());
        //Models'te oluşturduğumuz CreateToken nesnesinin TokenCreateAdmin metodu ile bir tane Token yarattık.
    }

    [Authorize]
    [HttpGet("[action]")]
    public IActionResult Test2()
    {
        return Ok("Hoşgeldiniz");
    }

    [Authorize(Roles = "Admin,Visitor")]
    [HttpGet("[action]")]
    public IActionResult Test3()
    {
        return Ok("Token İşlemi Başarılı");
    }
}


