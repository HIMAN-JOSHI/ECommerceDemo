using ECommerceDemo.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceDemo.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;

        public CartController(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst("sub")?.Value;
            var cart = await _httpClient.GetFromJsonAsync<CartDto>($"api/cart/{userId}");
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId) {

            var userId = User.FindFirst("sub")?.Value;
            var response = await _httpClient.PostAsJsonAsync($"api/cart/add", new { UserId = userId, ProductId = productId });

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Product added to cart successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to add product to cart.";
            }

            return RedirectToAction("Index", "Product");
        }
    }
}
