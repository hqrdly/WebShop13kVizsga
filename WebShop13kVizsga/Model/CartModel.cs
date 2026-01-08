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


        #region CartInventory
        public IEnumerable<CartDto> GetCart(int id)
        {
            if (!_context.Carts.Any(x => x.CartId == id))
            {
                throw new InvalidOperationException("Nincs ilyen kosár");
            }

            return _context.Carts
                .Where(x => x.CartId == id)
                .Select(x => new CartDto
                {
                    itemName = x.ItemName,
                    price = x.Price,
                    quantity = x.Quantity
                })
                .ToList();
        }
        #endregion

        #region CartTotalPrice
        public int GetCartTotalPrice(int id)
        {
            if (!_context.Carts.Any(x => x.CartId == id))
            {
                throw new InvalidOperationException("Nincs ilyen kosár");
            }
            return _context.Carts
                .Where(x => x.CartId == id)
                .Sum(x => x.Price * x.Quantity);
        }
        #endregion

        #region ModifyCart, (ModifyCartTotalPrice)
        public void ModifyCart(int id, ModifyCartDto dto)
        {
            if (!_context.Carts.Any(x => x.CartId == id))
            {
                throw new InvalidOperationException("Nincs ilyen kosár");
            }
            else
            {
                using var trx = _context.Database.BeginTransaction();
                try
                {
                    var cart = _context.Carts.First(x => x.CartId == id);
                    cart.ItemName = dto.Modif_ItemName;
                    cart.Price = dto.Modif_Price;
                    cart.Quantity = dto.Modif_Quantity;
                    _context.SaveChanges();
                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            } 
        }
        #endregion

        #region ModifyCartStatus

        #endregion

        #region ModifyItemQuantity
        public void ModifyItemQuantity(int id, int quantity)
        {
            if (!_context.Carts.Any(x => x.CartId == id))
            {
                throw new InvalidOperationException("Nincs ilyen kosár");
            }
            else
            {
                using var trx = _context.Database.BeginTransaction();
                try
                {
                    var cart = _context.Carts.First(x => x.CartId == id);
                    cart.Quantity = quantity;
                    _context.SaveChanges();
                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region DeleteItemFromCart
        public void DeleteItemFromCart(int id)
        {
            if (!_context.Carts.Any(x => x.CartId == id))
            {
                throw new InvalidOperationException("Nincs ilyen kosár");
            }
            else
            {
                using var trx = _context.Database.BeginTransaction();
                try
                {
                    var cart = _context.Carts.First(x => x.CartId == id);
                    _context.Carts.Remove(cart);
                    _context.SaveChanges();
                    trx.Commit();
                }
                catch
                {
                    trx.Rollback();
                    throw;
                }
            }
        }
        #endregion
    }
}