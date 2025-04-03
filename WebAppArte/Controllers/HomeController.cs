using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using WebAppArte.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebAppArte.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7021/api");
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Artist()
        {

            List<Artist> artistLst = new List<Artist>();
            
            return View(artistLst);
        }
        public async Task<IActionResult> GetArtist()
        {
          
            List<Artist> artistLst=new List<Artist>() ;
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress+"/artist/GetArtist");

            if (response.IsSuccessStatusCode)
            {
                //listaArtista = await response.Content.ReadAsStringAsync();
                string data=  response.Content.ReadAsStringAsync().Result;
                artistLst = JsonConvert.DeserializeObject<List<Artist>>(data);
            }
           
            return View("Artist",artistLst);
        }


        public IActionResult Edit(Artist artist)
        {
            return View(artist);
        }


        public async Task<IActionResult> PostArtist(Artist artist)
        {
            
            Artist artistResponse = new Artist();
            
            HttpContent content = new StringContent(JsonConvert.SerializeObject(artist), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/artist/PostArtist", content);


            if (response.IsSuccessStatusCode)
            {
                //listaArtista = await response.Content.ReadAsStringAsync();
                string data = response.Content.ReadAsStringAsync().Result;
                artistResponse = JsonConvert.DeserializeObject<Artist>(data);
              
            }

            return View("Edit", artistResponse);
         
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
    }
}