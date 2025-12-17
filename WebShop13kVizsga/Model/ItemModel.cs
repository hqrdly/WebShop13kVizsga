using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Model
{
    public class ItemModel
    {
        private readonly DataDbContext _context;
        public ItemModel(DataDbContext context)
        {
            _context = context;
        }



    }
}
