using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Dto
{
    public class NewCategoryDto
    {
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public List<Item> Items { get; set; } = new();
    }
}
