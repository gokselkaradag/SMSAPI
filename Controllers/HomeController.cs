using Microsoft.AspNetCore.Mvc;
using SMS_API.Models;
using System.Diagnostics;
using System.Text;

namespace SMS_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SMS()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SMS(string x)
        {
            var client = _httpClientFactory.CreateClient();

            var body = @"{
                ""api_id"": ""8c848fdcbca78e1415927cb5"",
                ""api_key"": ""1349816275549ae0f969876f"",
                ""sender"": ""gokselkrdg"",
                ""message_type"": ""turkce"",
                ""message"":""Destek talebini aldýk. En kýsa zamanda iletiþime geçeceðiz."",
                ""message_content_type"":""bilgi"",
                ""phones"": [""+905319683479""]
            }";

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://api.toplusmspaketleri.com/api/v1/1toN", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Message"] = "SMS Gönderildi";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "SMS Gönderilemedi";
            return RedirectToAction("Index");
        }
    }
}
