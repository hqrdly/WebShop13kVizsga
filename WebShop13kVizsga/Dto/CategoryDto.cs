using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Dto
{
    public class CategoryDto
    {
        public string categoryName { get; set; }
        public List<Item> items { get; set; }
    }
}
