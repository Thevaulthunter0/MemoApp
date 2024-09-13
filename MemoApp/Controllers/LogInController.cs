using MemoApp.Models;
using MemoApp.Models.Dto;
using MemoApp.Models.Object;
using MemoApp.Services;
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
            if(userId != null && userId != 0)
            {
                return RedirectToAction("Index", "Employee", new { IdEmployee = userId - 1 });
            }
            if(userId == 0)
            {
                return RedirectToAction("Index", "Moderator");
            }
            else { return View(model); }
        }
    }
}
