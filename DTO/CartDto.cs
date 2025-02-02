namespace ECommerceDemo.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public string userId { get; set; }

        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    { 
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
