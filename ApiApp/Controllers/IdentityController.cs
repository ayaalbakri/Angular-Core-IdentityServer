﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class IdentityController : Controller
    {
        //private static HttpClient _client = new HttpClient();
        [HttpGet]
        public IActionResult Get()
        {
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
            //Console.WriteLine(tokenResponse.Json);
            var token = tokenResponse.AccessToken;
            var custom = tokenResponse.Json.TryGetString("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            var xxxxxxxxxx = new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
