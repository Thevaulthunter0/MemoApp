using MemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MemoApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int UserId)
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
