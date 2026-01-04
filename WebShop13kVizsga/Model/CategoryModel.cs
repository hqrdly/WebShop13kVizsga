using Microsoft.EntityFrameworkCore;
using WebShop13kVizsga.Dto;
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

        public IEnumerable<CategoryDto> GetCategory()
        {
            return _context.Categories
                .OrderBy(x => x.CategoryName)
                .Select(x => new CategoryDto
                {
                    categoryName = x.CategoryName
                })
                .ToList();
        }

        public void CreateNewCategory(NewCategoryDto dto)
        {
            if (_context.Categories.Any(x => x.CategoryName == dto.Category_Name))
            {
                throw new InvalidOperationException("Ez a Category már létezik!");
            }

            using var trx = _context.Database.BeginTransaction();
            try
            {
                _context.Categories.Add(new Category
                {
                    CategoryName = dto.Category_Name,
                    Items = dto.Items
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

        public void ModifyCategory(int id, ModifyCategoryDto dto)
        {
            using var trx = _context.Database.BeginTransaction();
            try
            {
                var category = _context.Categories.First(x => x.CategoryId == id);
                category.CategoryName = dto.Modif_CategoryName;
                _context.SaveChanges();
                trx.Commit();
            }
            catch
            {
                trx.Rollback();
                throw;
            }
        }

        public void DeleteCategory(int id)
        {
            using var trx = _context.Database.BeginTransaction();
            try
            {
                var category = _context.Categories.First(x => x.CategoryId == id);
                _context.Categories.Remove(category);
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
}