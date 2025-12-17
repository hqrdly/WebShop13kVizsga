namespace WebShop13kVizsga.Dto
{
    public class OrderDto
    {
        public int cartId { get; set; }

        public int userId { get; set; }

        public int totalPrice { get; set; }

        public string targetAddress { get; set; }

        public DateTimeOffset date { get; set; }
    }
}
