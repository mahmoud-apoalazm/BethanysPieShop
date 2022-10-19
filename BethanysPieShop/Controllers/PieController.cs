using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICatogeryRepository _catogeryRepository;
        public PieController(IPieRepository pieRepository, ICatogeryRepository catogeryRepository)
        {
            _pieRepository = pieRepository;
            _catogeryRepository = catogeryRepository;
        }

        //public IActionResult List()
        //{
        //    //ViewBag.CurrentCategory = "Cheese Cakes";

        //    //return View(_pieRepository.AllPies);
        //    PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "All Pies");
        //    return View(pieListViewModel);
        //}

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _catogeryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PieListViewModel(pies, currentCategory));
        }

        public IActionResult Details(int id)
        {
            var pie =_pieRepository.GetPieById(id);
            if(pie == null)
                return NotFound();
            return View(pie);
            
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
