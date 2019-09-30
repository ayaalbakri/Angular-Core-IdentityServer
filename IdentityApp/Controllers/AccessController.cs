using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace IdentityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        // GET: api/Access
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            var tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = "custom",
                ClientId = "Aya",
                ClientSecret = "secret",
                Scope = "apiApp Rolesz",
                UserName = "aya@gmail.com",
                Password = "P@ssw0rd",
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
            return new List<string> { "Hi" };
        }

        // GET: api/Access/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Access
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Access/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
