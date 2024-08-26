using MemoApp.Models;
using MemoApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemoApp.Controllers
{
    public class LogInController : Controller
    {
        private readonly DbService DbService;

        public LogInController(DbService dbContext)
        {
            DbService = dbContext;
        }

        /// <summary>
        /// Render the sign in page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignIn()
        {
            var model = new UserAuthentificationDto();
            return View();
        }
        /// <summary>
        /// Fetch the data in the view, if user exist redirect to main page.
        /// If not redirect to retry.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignIn(UserAuthentificationDto model)
        {
            var users = await DbService.GetUsers();
            int? userId = await MemoApp.Models.Object.User.Authenticate(users, model);
            if(userId != null)
            {
                return RedirectToAction("Home", "Index", userId)
;           }
            else { return View(model); }
        }
    }
}
