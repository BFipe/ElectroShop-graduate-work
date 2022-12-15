using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Hello(List<int> number)
        {
            Console.WriteLine("Hello");
            foreach (var item in number)
            {
                Console.WriteLine(item);
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Index(List<int> number)
        {
            Console.WriteLine("Index");
            foreach (var item in number)
            {
                Console.WriteLine(item);
            }
            return View();
        } 
              

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}