using Microsoft.AspNetCore.Mvc;
using MemoApp.Models.Dto;
using MemoApp.Models.Object;

namespace MemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbService DbService;

        public EmployeeController(DbService dbContext)
        {
            DbService = dbContext;
        }

        public IActionResult Index(int IdEmployee)
        {
            var employee = DbService.getEmployee(IdEmployee).Result;
            EmployeeHomePageDto EmployeeData = new EmployeeHomePageDto()
            {
                Name = employee.Name,
                Jobs = DbService.getJobOfEmployee(IdEmployee).Result,
                MemosAssigned = DbService.getMemoAssigned(IdEmployee).Result,
                MemosCreated = DbService.getMemoCreated(employee.Name).Result,
            };
            var model = EmployeeData;
            return View(model);
        }
    }
}
