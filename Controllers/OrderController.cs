using Microsoft.AspNetCore.Mvc;

namespace ECommerceDemo.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            // Get the user's ID from the authenticated token
            var userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "User not authenticated.";
                return RedirectToAction("Index", "Account");

            }

            // Send the userId to the backend
            var orderRequest = new
            {
                UserId = userId
            };

            var response = await _httpClient.PostAsJsonAsync("place-order-url", orderRequest);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Order placed successfully.";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData["Error"] = "Failed to place order.";
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}
