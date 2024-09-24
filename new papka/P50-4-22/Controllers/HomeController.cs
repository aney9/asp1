using Microsoft.AspNetCore.Mvc;
using P50_4_22.Models;
using System.Diagnostics;

namespace P50_4_22.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View();
        }
        public IActionResult Bucket()
        {
            return View();
        }
        public IActionResult Map()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
