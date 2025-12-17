using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Model
{
    public class CategoryModel
    {
        private readonly DataDbContext _context;
        public CategoryModel(DataDbContext context)
        {
            _context = context;
        }



    }
}
