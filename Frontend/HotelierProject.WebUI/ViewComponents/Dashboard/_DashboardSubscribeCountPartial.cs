using HotelierProject.WebUI.Dtos.FollowersDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelierProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardSubscribeCountPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //INSTAGRAM
            var instagramClient = new HttpClient();
            var instagramRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://instagram-profile1.p.rapidapi.com/getprofileinfo/firatkazak"),
                Headers =
    {
        { "X-RapidAPI-Key", "4c49e28053mshffc9143870e45dbp13c64bjsn4d7d00e7c28e" },
        { "X-RapidAPI-Host", "instagram-profile1.p.rapidapi.com" },
    },
            };
            using (var instagramResponse = await instagramClient.SendAsync(instagramRequest))
            {
                instagramResponse.EnsureSuccessStatusCode();
                var instagramBody = await instagramResponse.Content.ReadAsStringAsync();
                ResultInstagramFollowersDto resultInstagramFollowersDtos = JsonConvert.DeserializeObject<ResultInstagramFollowersDto>(instagramBody);
                ViewBag.InstagramFollowers = resultInstagramFollowersDtos.followers;
                ViewBag.InstagramFollowing = resultInstagramFollowersDtos.following;
            }
            //TWITTER
            var twitterClient = new HttpClient();
            var twitterRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twitter32.p.rapidapi.com/getProfile?username=firatkazak"),
                Headers =
    {
        { "X-RapidAPI-Key", "4c49e28053mshffc9143870e45dbp13c64bjsn4d7d00e7c28e" },
        { "X-RapidAPI-Host", "twitter32.p.rapidapi.com" },
    },
            };
            using (var twitterResponse = await twitterClient.SendAsync(twitterRequest))
            {
                twitterResponse.EnsureSuccessStatusCode();
                var twitterBody = await twitterResponse.Content.ReadAsStringAsync();
                ResultTwitterFollowersDto resultTwitterFollowersDto = JsonConvert.DeserializeObject<ResultTwitterFollowersDto>(twitterBody);
                ViewBag.TwitterFollowers = resultTwitterFollowersDto.data.user_info.followers_count;
                ViewBag.TwitterFollowing = resultTwitterFollowersDto.data.user_info.friends_count;
            }
            //LINKEDIN
            var linkedinClient = new HttpClient();
            var linkedinRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://fresh-linkedin-profile-data.p.rapidapi.com/get-linkedin-profile?linkedin_url=https%3A%2F%2Fwww.linkedin.com%2Fin%2Ffiratkazak%2F"),
                Headers =
    {
        { "X-RapidAPI-Key", "4c49e28053mshffc9143870e45dbp13c64bjsn4d7d00e7c28e" },
        { "X-RapidAPI-Host", "fresh-linkedin-profile-data.p.rapidapi.com" },
    },
            };
            using (var linkedinResponse = await linkedinClient.SendAsync(linkedinRequest))
            {
                linkedinResponse.EnsureSuccessStatusCode();
                var linkedinBody = await linkedinResponse.Content.ReadAsStringAsync();
                ResultLinkedinFollowersDto resultLinkedinFollowersDto = JsonConvert.DeserializeObject<ResultLinkedinFollowersDto>(linkedinBody);
                ViewBag.LinkedinFollowers = resultLinkedinFollowersDto.data.followers_count;
                ViewBag.LinkedinFollowing = resultLinkedinFollowersDto.data.connections_count;
            }
            return View();
        }
    }
}
