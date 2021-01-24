using CountrySorter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace CountrySorter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Sorted()
        {
            var model = await GetCountryData();
            //sort by country first, put it in same list
            model = model.OrderBy(model => model.name).ToList();
            //sort by sub region next, put it in same list
            model = model.OrderBy(model => model.subregion).ToList();
            //sort by region, put in the same list
            model = model.OrderBy(model => model.region).ToList();
            var result = JsonConvert.SerializeObject(model);
            // Pass the data into the View
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<CountryData>> GetCountryData()
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "https://restcountries.eu";
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                // If the API call fails, call it again according to the re-try policy
                // specified in Startup.cs
                var result = await client.GetAsync("/rest/v2/all");

                if (result.IsSuccessStatusCode)
                {
                    // Read all of the response and deserialise it into an instace of
                    // WeatherForecast class
                    var content = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CountryData>>(content);
                }
            }          
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetCountryData();
            // Pass the data into the View
            return View(model);
        }
    }
}
