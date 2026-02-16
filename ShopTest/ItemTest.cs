using WebShop13kVizsga.Model;
using WebShop13kVizsga.Persistence;

namespace ShopTest
{
    public class ItemTest
    {
        private readonly ItemModel _model;
        private readonly DataDbContext _context;

        public ItemTest()
        {
            _context = DbContextFactory.Create();
            _model = new ItemModel(_context);
        }

        [Fact]
        public void AllItems_Valid()
        {
            var result = _model.GetItems();

            Assert.NotEmpty(result);
            Assert.All(result, x=> Assert.True(x.itemId > 0));
            Assert.All(result, x=> Assert.False(string.IsNullOrWhiteSpace(x.itemName)));
        }


                            
    }
}