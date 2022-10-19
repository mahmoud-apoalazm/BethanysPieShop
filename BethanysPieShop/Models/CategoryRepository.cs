
namespace BethanysPieShop.Models
{
    public class CategoryRepository:ICatogeryRepository
    {
        private readonly BethanyPieShopDbContext _bethanyPieShopDbContext;
        public CategoryRepository(BethanyPieShopDbContext bethanyPieShopDbContext)
        {
            _bethanyPieShopDbContext = bethanyPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _bethanyPieShopDbContext.Categories
            .OrderBy(c => c.CategoryName);
    }
}
