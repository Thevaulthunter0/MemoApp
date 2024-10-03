using Microsoft.AspNetCore.Mvc;
using MemoApp.Models.Dto;
using MemoApp.Models.Object;
using MemoApp.Services;

namespace MemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbService DbService;
        private UserService UserService;

        public EmployeeController(DbService dbContext, UserService userService)
        {
            DbService = dbContext;
            UserService = userService;
        }

        public async Task<IActionResult> Index(int IdEmployee)
        {
            var employee  = DbService.getEmployee(IdEmployee).Result;
            UserService.SetActiveEmployee(employee);
            EmployeeHomePageDto EmployeeData = new EmployeeHomePageDto()
            {
                Name = employee.Name,
                Jobs = DbService.getJobOfEmployee(IdEmployee).Result,
                MemosAssigned = DbService.getMemoAssigned(IdEmployee).Result,
                MemosCreatedCount = DbService.getMemoCreatedCount(employee.Name).Result,
            };
            var model = EmployeeData;
            return View(model);
        }

        public async Task<IActionResult> Disconnect()
        {
            UserService.UnSetActiveEmployee();
            return RedirectToAction("SignIn", "LogIn");
        }
    }
}
