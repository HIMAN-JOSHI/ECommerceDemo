using Microsoft.AspNetCore.Mvc;

using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using ECommerceDemo.Models;
using ECommerceDemo.DTO;

namespace ECommerceDemo.Controllers
{
    public class AccountController : Controller
    { 
        private readonly HttpClient _httpClient;
        private readonly string _webServiceBaseUrl;

        public AccountController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webServiceBaseUrl = configuration["WebService:BaseUrl"];

        }

        // GET: Registration Page
        [HttpGet]
        public IActionResult Register()
        { 
            return View();
        }

        // POST: Handle Registration
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model) {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_webServiceBaseUrl}/api/authenticate/register", content);

            if (response.IsSuccessStatusCode) {

                TempData["SuccessMessage"] = "Registration successful! You can now login.";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "Registration failed. Please try again.";
            return View(model);
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_webServiceBaseUrl}/api/authenticate/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

                // Store the token (e.g., in session or cookies)
                HttpContext.Session.SetString("JWT", tokenResponse.Token);

                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Login Failed. Please check your credentials.";
            return View(model);

        }

        // POST: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }

    //public class AccountController : Controller
    //{
        //private readonly HttpClient _httpClient;
        //private readonly string _webServiceBaseUrl;

        //public AccountController(HttpClient httpClient) { 
        
        //    _httpClient = httpClient;
        //}

        //public IActionResult Register() => View();

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model) { 
        
        //    if(!ModelState.IsValid) 
        //        return View(model);
            
        //    var jsonContent = JsonSerializer.Serialize(model);
        //    var stringContent = new StringContent(jsonContent);
        //    stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json")
        //    {
        //        CharSet = Encoding.UTF8.WebName
        //    };

        //    var response = await _httpClient.PostAsync("", stringContent);

        //    if(response.IsSuccessStatusCode)
        //        return RedirectToAction("Login");

        //    ModelState.AddModelError("", "Registration failed.");
        //    return View(model);
        //}

        //public IActionResult Login() => View();

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{ 
        //    if(!ModelState.IsValid)
        //        return View(model);

        //    var jsonContent = JsonSerializer.Serialize(model);
        //    var content = new StringContent(jsonContent);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json")
        //    {
        //        CharSet = Encoding.UTF8.WebName
        //    };

        //    var response = await _httpClient.PostAsync("", content);

        //    if (response.IsSuccessStatusCode)
        //    { 
        //        var token = await response.Content.ReadAsStringAsync();
        //        HttpContext.Session.SetString("token", token); // Store JWT
        //        return RedirectToAction("Index", "Product");

        //    }

        //    ModelState.AddModelError("", "Login Failed.");
        //    return View(model);

        //}
    //}
}
