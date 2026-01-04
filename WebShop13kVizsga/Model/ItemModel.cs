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
        public IEnumerable<ItemDto> GetItems()
        {
            return _context.Items
                .OrderBy(x => x.ItemName)
                .Select(x => new ItemDto
                {
                    categoryId = x.CategoryId,
                    itemName = x.ItemName,
                    price = x.Price,
                    description = x.Description,
                    quantity = x.Quantity
                })
                .ToList();
        }
        #endregion

        #region CategorySelectItems
        public IEnumerable<ItemDto> CategorySelectItems(int id)
        {
            if (!_context.Categories.Any(x => x.CategoryId == id))
            {
                throw new InvalidOperationException("Nincs ilyen kategoria");
            }

            return _context.Items
                .Where(x => x.CategoryId == id)
                .Select(x => new ItemDto
                {
                    categoryId = x.CategoryId,
                    itemName = x.ItemName,
                    price = x.Price,
                    description = x.Description,
                    quantity = x.Quantity
                })
                .ToList();
        }
        #endregion

        #region CreateNewItem
        public void CreateNewItem(ItemDto itemDto)
        {
            var category = _context.Categories
                .FirstOrDefault(x => x.CategoryId == itemDto.categoryId);

            if (category == null)
                throw new InvalidOperationException("Nincs ilyen kategoria");

            using var trx = _context.Database.BeginTransaction();
            try
            {
                _context.Items.Add(new Item
                {
                    ItemName = itemDto.itemName,
                    Price = itemDto.price,
                    Description = itemDto.description,
                    Quantity = itemDto.quantity,
                    CategoryId = category.CategoryId
                });

                _context.SaveChanges();
                trx.Commit();
            }
            catch
            {
                trx.Rollback();
                throw;
            }
        }
        #endregion

        #region ModifyItem
        public void ModifyItem(int id, ModifyItemDto itemDto)
        {
            if (!_context.Items.Any(x => x.ItemId == id))
            {
                throw new InvalidOperationException("Nem létező itemid");
            }

            var category = _context.Categories
                .FirstOrDefault(x => x.CategoryId == itemDto.Modif_CategoryId);

            if (category == null)
                throw new InvalidOperationException("Nincs ilyen kategória");

            using var trx = _context.Database.BeginTransaction();
            try
            {
                var item = _context.Items.First(x => x.ItemId == id);

                item.ItemName = itemDto.itemName;
                item.Description = itemDto.description;
                item.Price = itemDto.price;
                item.Quantity = itemDto.quantity;
                item.CategoryId = category.CategoryId;

                _context.SaveChanges();
                trx.Commit();
            }
            catch
            {
                trx.Rollback();
                throw;
            }
        }
        #endregion

        #region DeleteItem
        public void DeleteItem(int id)
        {
            if (!_context.Items.Any(x => x.ItemId == id))
            {
                throw new InvalidOperationException("Nincs ilyen itemid");
            }

            using var trx = _context.Database.BeginTransaction();
            try
            {
                var item = _context.Items.First(x => x.ItemId == id);
                _context.Items.Remove(item);
                _context.SaveChanges();
                trx.Commit();
            }
            catch
            {
                trx.Rollback();
                throw;
            }
        }
        #endregion
    }
}