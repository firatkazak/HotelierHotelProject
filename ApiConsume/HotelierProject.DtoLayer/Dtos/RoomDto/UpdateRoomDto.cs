using System.ComponentModel.DataAnnotations;

namespace HotelierProject.DtoLayer.Dtos.RoomDto
{
    public class UpdateRoomDto
    {
        public int RoomID { get; set; }

        [Required(ErrorMessage = "Lütfen oda numarasını giriniz")]
        public string RoomNumber { get; set; }
        
        [Required(ErrorMessage = "Lütfen oda görselini giriniz")]
        public string RoomCoverImage { get; set; }
        
        [Required(ErrorMessage = "Lütfen oda fiyatını giriniz")]
        public int Price { get; set; }
        
        [Required(ErrorMessage = "Lütfen oda başlığını giriniz")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Lütfen odanın yatak sayısını giriniz")]
        public string BedCount { get; set; }
        
        [Required(ErrorMessage = "Lütfen odanın banyo sayısını giriniz")]
        public string BathCount { get; set; }
        public string Wifi { get; set; }
        [Required(ErrorMessage = "Lütfen oda açıklamasını giriniz")]
        [StringLength(100,ErrorMessage ="En fazla 100 karakterlik açıklama girebilirsiniz")]
        public string Description { get; set; }
    }
}
