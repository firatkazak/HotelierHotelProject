using HotelierProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelierProject.WebUI.Controllers;

public class StaffController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;//HttpClientFactory'den örnek aldık. 
    //NOT: Program.cs'de builder.Services.AddHttpClient(); ile Http İstemcisinin ayarını geçtik.
    public StaffController(IHttpClientFactory httpClientFactory)//HttpClientFactory örneğini Constructor'da geçtik.
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()//Burada veri JSON olarak geliyor. O yüzden Deserialize edeceğiz.
    {
        HttpClient client = _httpClientFactory.CreateClient();//HttpClient sınıfını kullanarak HTTP istekleri göndermek için bir client oluşturduk.
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5160/api/Staff");//Belirtilen URL'ye GET isteği gönderilir.
        if (responseMessage.IsSuccessStatusCode)//İsteğin başarılı bir şekilde gerçekleşip gerçekleşmediği kontrol edilir.
        {
            string jsonData = await responseMessage.Content.ReadAsStringAsync();//Başarılı ise, isteğin içeriği okunur (JSON formatında).
            List<StaffViewModel>? values = JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData);//JSON verisi, StaffViewModel türündeki bir liste haline getirilir.
            return View(values);//Bu liste, View'a iletilir ve sayfa render edilir.
        }
        return View();//Eğer istek başarısız olursa, aynı sayfaya tekrar View döndürülür.
    }

    [HttpGet]
    public IActionResult AddStaff()
    {
        return View();//AddStaff View'ini döndürecek.
    }

    [HttpPost]
    public async Task<IActionResult> AddStaff(AddStaffViewModel model)//Burada bir data gönderiyoruz bu data JSON'a dönüşecek. O yüzden Serialize edeceğiz.
    {
        HttpClient client = _httpClientFactory.CreateClient();//HttpClient sınıfını kullanarak HTTP istekleri göndermek için bir client oluşturduk.
        string jsonData = JsonConvert.SerializeObject(model);//Modeldeki bilgileri JSON formatına dönüştürülüyor.
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");//StringContent, JSON verisi içeren HTTP içeriği oluşturmak için kullanılır.
        HttpResponseMessage responseMessage = await client.PostAsync("http://localhost:5160/api/Staff", stringContent);//Oluşturulan JSON içeriğini içeren POST isteği gönderdik.
        if (responseMessage.IsSuccessStatusCode)//İsteğin başarılı bir şekilde gerçekleşip gerçekleşmediği kontrol ediliyor.
        {
            return RedirectToAction("Index");//Eğer başarılı ise, Index sayfasına yönlendirme yapılır.
        }
        return View();//Eğer istek başarısız olursa, aynı sayfaya tekrar View döndürülür.
    }

    [HttpGet]
    public async Task<IActionResult> UpdateStaff(int id)
    {
        HttpClient client = _httpClientFactory.CreateClient();//HttpClient sınıfını kullanarak HTTP istekleri göndermek için bir client oluşturduk.
        HttpResponseMessage responseMessage = await client.GetAsync($"http://localhost:5160/api/Staff/{id}");//Belirtilen URL'ye GET isteği gönderilir.
        if (responseMessage.IsSuccessStatusCode)//İsteğin başarılı bir şekilde gerçekleşip gerçekleşmediği kontrol ediliyor.
        {
            string jsonData = await responseMessage.Content.ReadAsStringAsync();//Başarılı ise, API'den JSON verisi alınır.
            UpdateStaffViewModel? values = JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData);//Ve TestimonialViewModel türüne dönüştürülür.
            return View(values);//Dönüştürülen veri, güncelleme formu için kullanılan View'a iletilir.
        }
        return View();//Eğer istek başarısız olursa, boş bir View döndürülür.
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStaff(UpdateStaffViewModel model)//Modele göre güncelleme yapacağı için model parametresi verdik.
    {
        HttpClient client = _httpClientFactory.CreateClient();//HttpClient sınıfını kullanarak HTTP istekleri göndermek için bir client oluşturduk.
        string jsonData = JsonConvert.SerializeObject(model);//Modeldeki bilgileri JSON formatına dönüştürülüyor.
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");//StringContent, JSON verisi içeren HTTP içeriği oluşturmak için kullanılır.
        HttpResponseMessage responseMessage = await client.PutAsync("http://localhost:5160/api/Staff/", stringContent);//Oluşturulan JSON içeriğini içeren PUT isteği gönderilir.
        if (responseMessage.IsSuccessStatusCode)//İsteğin başarılı bir şekilde gerçekleşip gerçekleşmediği kontrol ediliyor.
        {
            return RedirectToAction("Index");//Eğer başarılı ise, Index sayfasına yönlendirme yapılır.
        }
        return View();//Eğer istek başarısız olursa, aynı sayfaya tekrar View döndürülür.
    }

    public async Task<IActionResult> DeleteStaff(int id)//Id'ye göre silme işlemi yapacak.
    {
        HttpClient client = _httpClientFactory.CreateClient();//1 tane istemci oluşturduk.
        HttpResponseMessage responseMessage = await client.DeleteAsync($"http://localhost:5160/api/Staff/{id}");//Verilen Id'deki Staff'ı silmek için API'ye DELETE isteği atar.
        if (responseMessage.IsSuccessStatusCode)//İsteğin başarılı bir şekilde gerçekleşip gerçekleşmediği kontrol ediliyor.
        {
            return RedirectToAction("Index");//Eğer başarılı ise, Index sayfasına yönlendirme yapılır.
        }
        return View();//Eğer istek başarısız olursa, aynı sayfaya tekrar View döndürülür.
    }
}
