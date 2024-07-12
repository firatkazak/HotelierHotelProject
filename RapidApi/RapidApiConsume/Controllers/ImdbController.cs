using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidApiConsume.Models;

namespace RapidApiConsume.Controllers;

public class ImdbController : Controller
{
    public async Task<IActionResult> Index()
    {
        List<ApiMovieViewModel>? apiMovieViewModels = new List<ApiMovieViewModel>();//Listeleme işlemi için bir örnek aldık.
        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
            Headers = { { "X-RapidAPI-Key", "4c49e28053mshffc9143870e45dbp13c64bjsn4d7d00e7c28e" }, { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" }, },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            apiMovieViewModels = JsonConvert.DeserializeObject<List<ApiMovieViewModel>>(body);
            //JsonConvert ile body'deki Json verilerini ApiMovieViewModel sınıfının Listesine dönüştürür. Bunu apiMovieViewModels'a ata.
            return View(apiMovieViewModels);//apiMovieViewModels'ı dönecek.
            //NOT: Kodun geri kalan kısmını RapidApi verdi.
        }
    }
}
