using AutoMapper;
using HotelierProject.BusinessLayer.Abstract;
using HotelierProject.DtoLayer.Dtos.RoomDto;
using HotelierProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Room2Controller : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public Room2Controller(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values = _roomService.TGetList();//Oda servisinden tüm odaların listesini alır.
            return Ok(values);//Bu liste, Ok() metodu ile birlikte HTTP 200 (OK) durumunda geri döndürülür.
        }

        [HttpPost]
        public IActionResult AddRoom(RoomAddDto roomAddDto)//Oda ekleme işlemini RoomAddDto'dan yapacağımız için bir örnek aldık.
        {
            if (!ModelState.IsValid)//Gelen isteğin model durumu geçerli değilse;
            {
                return BadRequest();//BadRequest döndür.
            }
            //Gelen isteğin model durumu geçerli ise;
            Room values = _mapper.Map<Room>(roomAddDto);//RoomAddDto türündeki veriyi, _mapper kullanarak Room türüne dönüştür.
            _roomService.TInsert(values);//Dönüştürülmüş veriyi _roomService.TInsert() metodu ile ekle.
            return Ok(values);//HTTP 200 (OK) durumunda geri döndür.
        }

        [HttpPut]
        public IActionResult UpdateRoom(UpdateRoomDto updateRoomDto)//Oda güncelleme işlemini UpdateRoomDto'dan yapacağımız için bir örnek aldık.
        {
            if (!ModelState.IsValid)//Gelen isteğin model durumu geçerli değilse;
            {
                return BadRequest();//BadRequest döndür.
            }
            //Gelen isteğin model durumu geçerli ise;
            Room values = _mapper.Map<Room>(updateRoomDto);//UpdateRoomDto türündeki veriyi, _mapper kullanarak Room türüne dönüştürür.
            _roomService.TUpdate(values);//Dönüştürülmüş veriyi _roomService.TUpdate() metodu ile güncelle
            return Ok(values);//HTTP 200 (OK) durumunda geri döndür.
        }
    }
}



