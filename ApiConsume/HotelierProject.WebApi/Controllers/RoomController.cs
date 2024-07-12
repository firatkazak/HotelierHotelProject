using HotelierProject.BusinessLayer.Abstract;
using HotelierProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebApi.Controllers;

[Route("api/[controller]")]//Controller'ın Root'u. api/Room'a gidecek API'de.
[ApiController]//ApiController olduğunu belirten Attribute.

public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;//BL'deki IRoomService'den bir tane örnek aldık.

    public RoomController(IRoomService roomService)//Constructor oluşturduk.
    {
        _roomService = roomService;
    }

    [HttpGet]//Veri getirmek istiyorsak Metodu HttpGet ile işaretlemeliyiz.
    public IActionResult RoomList()
    {
        var values = _roomService.TGetList();//values'in içine Odaları listele.
        return Ok(values);//values'i döndür, yani odaları listeli şekilde bize verecek.
    }

    [HttpPost]//Veri göndermek istiyorsak Metodu HttpPost ile işaretlemeliyiz.
    public IActionResult AddRoom(Room room)//Room Entity'sinden room örneği aldık çünkü bu şekilde oda ekleyeceğiz.
    {
        _roomService.TInsert(room);//odayı ekle.
        return Ok();//Ok döndür.
    }

    [HttpDelete("{id}")]//Veri silmek istiyorsak Metodu HttpDelete ile işaretlemeliyiz.
    //id'ye göre sileceğimiz için parantez içinde "{id}" şeklinde parametre vermeliyiz.
    public IActionResult DeleteRoom(int id)
    {
        var values = _roomService.TGetByID(id);//id'ye göre önce silinecek veriyi getiriyoruz. bunu values'a atıyoruz.
        _roomService.TDelete(values);//sonra values'ı yani silinecek odayı id'ye göre siliyoruz
        return Ok();//Ok döndürüyoruz.
    }

    [HttpPut]
    public IActionResult UpdateRoom(Room room)//Room Entity'sinden room örneği aldık çünkü bu şekilde oda güncelleyeceğiz.
    {
        _roomService.TUpdate(room);//room'u güncelle.
        return Ok();//Ok döndür.
    }

    [HttpGet("{id}")]//Yukarıdaki HttpGet'ten farkı parametre olarak id alıyor olması. Yani bu ID'ye göre getirecek.
    public IActionResult GetRoom(int id)//id'ye göre getireceğimiz için int tipinde id parametresi atıyoruz.
    {
        var values = _roomService.TGetByID(id);//id'ye göre odayı getir ve values'un içine at.
        return Ok(values);//getirilen values'u Ok'un içinde döndürüyoruz.
    }
}
