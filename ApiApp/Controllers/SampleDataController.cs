using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace WebApplicationBasic.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class SampleDataController : Controller
    {

        private static HttpClient _client = new HttpClient();
        private static string[] Summaries = new[]
        {

            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var xxxxx = new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = "custom",
                ClientId = "Aya",
                ClientSecret = "secret",
                Scope = "apiApp",
                //UserName = "aya@gmail.com",
                //Password = "P@ssw0rd",
                //Parameters =
                //     {
                //        { "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin"},
                //      { "scope", "apiApp" }
                //    }
            }).Result;

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}
            var xxx = tokenResponse.Json;
            Console.WriteLine(tokenResponse.Json);
            var token = tokenResponse.AccessToken;
            //var custom = tokenResponse.Json.TryGetString("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            //_client.BaseAddress = new Uri("https://3dcore-api.azurewebsites.net");
            //_client.BaseAddress = new Uri("http://localhost:5001");
            // HttpResponseMessage response = _client.GetAsync($"/api/Identity").Result;
            // string tempData = response.Content.ReadAsStringAsync().Result;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
