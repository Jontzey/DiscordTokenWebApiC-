using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace testingDeleteLater.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscordController : ControllerBase
    {
     
        private readonly HttpClient _httpClient;
        public DiscordController( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("GetAccessToken")]
        public IActionResult GetToken(string code)
        {
            
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code not found.");
            }
            else
            {
               string clId = Environment.GetEnvironmentVariable("client_id");
               string clSecret = Environment.GetEnvironmentVariable("client_secret");
                try
                {
                    //To do this you need to add enviroment variables, with client id and client secret and with its value from Discord dev portal
                    var formData = new Dictionary<string, string>{
                    {"client_id", clId},
                    {"client_secret", clSecret},
                    {"grant_type", "authorization_code"},
                    {"code", code},
                    {"redirect_uri", "https://localhost:7053/swagger/index.html"}};

                    var content = new FormUrlEncodedContent(formData);

                    var response = _httpClient.PostAsync("https://discord.com/api/oauth2/token", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(responseContent);
                        return Ok(responseContent);
                    }
                    else
                    {
                        return BadRequest(response);
                        
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }


        [HttpPost("Post AccessToken")]
        public IActionResult PostToken(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = _httpClient.GetAsync("https://discord.com/api/users/@me").Result;

            if (response.IsSuccessStatusCode)
            {
                var userData = response.Content.ReadAsStringAsync().Result;

                return Ok(userData);
            }
            else
            {
                return BadRequest(response.ToString());
            }
        }

        private class TokenResponse
        {
            public string access_token { get; set; }
   
        }
    }
}