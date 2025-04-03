using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppArte.Models;

namespace WebAppArte.Controllers
{
    public class PaintController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7021/api");
        private readonly HttpClient _httpClient;


        public PaintController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
     

        public async Task<IActionResult> Index(int ID)
        {

            List<Paint> PaintLst = new List<Paint>();
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/paint/GetAPaint?ID=" + ID);

            if (response.IsSuccessStatusCode)
            {
                //listaArtista = await response.Content.ReadAsStringAsync();
                string data = response.Content.ReadAsStringAsync().Result;
                PaintLst = JsonConvert.DeserializeObject<List<Paint>>(data);
            }

            return View("Index", PaintLst);
        }

        public async Task<IActionResult> Delete(int ID)
        {

            List<Paint> PaintLst = new List<Paint>();
            HttpResponseMessage response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "/paint/DeletePaint?ID=" + ID);

            if (response.IsSuccessStatusCode)
            {
                //listaArtista = await response.Content.ReadAsStringAsync();
               
            }

            return View("Index", PaintLst);
        }
    }
}
