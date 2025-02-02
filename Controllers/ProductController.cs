using ECommerceDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ECommerceDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient) { 
        
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode) {

                var json = await response.Content.ReadAsStringAsync();
                var cart = JsonSerializer.Deserialize<CartViewModel>(json);
                return View(cart);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity) {

            var token = HttpContext.Session.GetString("JWT");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(new { productId, quantity }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("add-to-cart-url", content);

            if (response.IsSuccessStatusCode) {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed to add product to cart.";
            return RedirectToAction("Index", "Product");

        }
    }
}
