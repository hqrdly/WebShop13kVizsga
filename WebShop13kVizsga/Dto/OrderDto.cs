using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public string TargetAddress { get; set; }
        public int TargetPhone { get; set; }
        public OrderStatus Status { get; set; }
        public DateTimeOffset Date { get; set; }

    }
}
