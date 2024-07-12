using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebUI.ViewComponents.Dashboard
{
    public class _DashboardWidgetPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardWidgetPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Staff
            var staffClient = _httpClientFactory.CreateClient();
            var staffResponseMessage = await staffClient.GetAsync("http://localhost:5160/api/DashboardWidgets/StaffCount");
            var staffJsonData = await staffResponseMessage.Content.ReadAsStringAsync();
            ViewBag.StaffCount = staffJsonData;
            //Booking
            var bookingClient = _httpClientFactory.CreateClient();
            var bookingResponseMessage = await bookingClient.GetAsync("http://localhost:5160/api/DashboardWidgets/BookingCount");
            var bookingJsonData = await bookingResponseMessage.Content.ReadAsStringAsync();
            ViewBag.BookingCount = bookingJsonData;
            //AppUser
            var appUserClient = _httpClientFactory.CreateClient();
            var appUserResponseMessage = await appUserClient.GetAsync("http://localhost:5160/api/DashboardWidgets/AppUserCount");
            var appUserJsonData = await appUserResponseMessage.Content.ReadAsStringAsync();
            ViewBag.AppUserCount = appUserJsonData;
            //Room
            var roomClient = _httpClientFactory.CreateClient();
            var roomResponseMessage = await roomClient.GetAsync("http://localhost:5160/api/DashboardWidgets/RoomCount");
            var roomJsonData = await roomResponseMessage.Content.ReadAsStringAsync();
            ViewBag.RoomCount = roomJsonData;

            return View();
        }
    }
}
