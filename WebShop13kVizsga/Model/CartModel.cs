using WebShop13kVizsga.Dto;
using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Model
{
    public class CartModel
    {
        private readonly DataDbContext _context;

        public CartModel(DataDbContext context)
        {
            _context = context;
        }

    }
}