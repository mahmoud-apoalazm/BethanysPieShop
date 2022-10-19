using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Components
{
    public class CategoryMenu :ViewComponent
    {
        private readonly ICatogeryRepository _catogeryRepository;

        public CategoryMenu(ICatogeryRepository catogeryRepository)
        {
            _catogeryRepository = catogeryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _catogeryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
