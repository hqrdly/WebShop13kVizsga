using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Model
{
    public class OrderModel
    {
        private readonly DataDbContext _context;
        public OrderModel(DataDbContext context)
        {
            _context = context;
        }


    }
}
