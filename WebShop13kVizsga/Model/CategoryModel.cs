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

        public IEnumerable <CategoryDto> GetCategory()
        {
            return _context.Categories.OrderBy(x => x.CategoryName).Select(x => new CategoryDto { categoryName = x.CategoryName});
        }

        public void CreateNewCategory(NewCategoryDto dto)
        {
            if (_context.Categories.Any(x => x.CategoryName == dto.Category_Name))
            {
                throw new InvalidOperationException("Ez a Category már létezik!");
            }
            using var trx = _context.Database.BeginTransaction();
            {
                _context.Categories.Add(new Category
                {
                    CategoryName = dto.Category_Name,
                    Items = dto.Items,
                });
                _context.SaveChanges();
                trx.Commit();
            }
        }

        public void ModifyCategory(int id, ModifyCategoryDto dto)
        {
            using var trx = _context.Database.BeginTransaction();
            {
                _context.Categories.Where(x => x.CategoryId==dto.Category_Id).First().CategoryName=dto.Modif_CategoryName;
                _context.SaveChanges();
                trx.Commit();
            }

        }

        public void DeleteCategory(int id)
        {
            using var trx = _context.Database.BeginTransaction();
            {
                _context.Remove(_context.Categories.Where(x => x.CategoryId == id).First());
                _context.SaveChanges();
                trx.Commit();
            }
        }

    }
}
