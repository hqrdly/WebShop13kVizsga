using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Dto
{
    public class ModifyCategoryDto
    {
        public string Modif_CategoryName { get; set; }
        public int Category_Id { get; set; }
        public List<Item> Modif_Items { get; set; }
    }
}
