using Microsoft.EntityFrameworkCore;
using WebShop13kVizsga.Dto;
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

        #region Items
        public IEnumerable<Dto.ItemDto> GetItems()
        {
            return _context.Items.OrderBy(x=> x.ItemName).Select(x=> new Dto.ItemDto { categoryId = x.ItemId, itemName = x.ItemName, price = x.Price, description = x.Description, quantity = x.Quantity });
        }
        #endregion

        #region CategorySelectItems
        public IEnumerable<Dto.ItemDto> CategorySelectItems(int id)
        {
            if (_context.Categories.Any(x => x.CategoryId != id))
            {
                throw new InvalidOperationException("Nincs ilyen kategória");
            }
            else
            {
                return _context.Items.Where(x => x.CategoryId == id).Select(x => new Dto.ItemDto { categoryId = x.ItemId, itemName = x.ItemName, price = x.Price, description = x.Description, quantity = x.Quantity });
            }
        }
        #endregion

        #region CreateNewItem
        public void CreateNewItem(Dto.ItemDto itemDto)
        {
            int categoryId = _context.Categories.Where(x => x.CategoryId == itemDto.categoryId).First().CategoryId;
            using var trx = _context.Database.BeginTransaction();
            {
                _context.Items.Add(new Item
                {
                    ItemName = itemDto.itemName,
                    Price = itemDto.price,
                    Description = itemDto.description,
                    Quantity = itemDto.quantity,
                    CategoryId = categoryId,
                });
            }
        }
        #endregion

        #region ModifyItem
        public void ModifyItem(int id, ModifyItemDto itemDto)
        {
            if (_context.Items.Any(x => x.ItemId != id))
            {
                throw new InvalidOperationException("Nem létező itemid");
            }
            else
            {
                int categoryId = _context.Categories.Where(x => x.CategoryId == itemDto.Modif_CategoryId).First().CategoryId;
                using var trx = _context.Database.BeginTransaction();
                {
                    _context.Items.Where(x=> x.ItemId == itemDto.itemId).First().ItemName = itemDto.itemName;
                    _context.Items.Where(x => x.ItemId == itemDto.itemId).First().Description = itemDto.description;
                    _context.Items.Where(x => x.ItemId == itemDto.itemId).First().Price = itemDto.price;
                    _context.Items.Where(x => x.ItemId == itemDto.itemId).First().Quantity = itemDto.quantity;
                    _context.Items.Where(x => x.ItemId == itemDto.itemId).First().CategoryId = categoryId;
                   _context.SaveChanges();
                    trx.Commit();

                }
            }
            
        }
        #endregion

        #region DeleteItem
        public void DeleteItem(int id)
        {
            if (_context.Items.Any(x => x.ItemId != id))
            {
                throw new InvalidOperationException("Nincs ilyen itemid");
            }
            else
            {
                using var trx = _context.Database.BeginTransaction();
                {
                    _context.Remove(_context.Items.Where(x => x.ItemId == id).First());
                    _context.SaveChanges();
                    trx.Commit();
                }
            }
        }
        #endregion

    }
}
