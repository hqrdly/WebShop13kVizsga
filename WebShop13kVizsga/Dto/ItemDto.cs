using System.ComponentModel.DataAnnotations;

namespace WebShop13kVizsga.Dto
{
    public class ItemDto
    {
        public int itemId {  get; set; }
        public int categoryId { get; set; }

        public string itemName { get; set; }

        public int quantity { get; set; }

        public string description { get; set; }

        public int price { get; set; }
    }
}
