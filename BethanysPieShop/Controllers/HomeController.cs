using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICatogeryRepository _catogeryRepository;

        public HomeController(IPieRepository pieRepository, ICatogeryRepository catogeryRepository)
        {
            _pieRepository = pieRepository;
            _catogeryRepository = catogeryRepository;
        }

        public IActionResult Index()
        {
            var piesOfTheWeek = _pieRepository.PiesOfTheWeek;
            var homeViewModel=new HomeViewModel(piesOfTheWeek);
            return View(homeViewModel);
        }
    }
}
