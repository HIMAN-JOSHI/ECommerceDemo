namespace ECommerceDemo.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class CartItemViewModel
    { 
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
