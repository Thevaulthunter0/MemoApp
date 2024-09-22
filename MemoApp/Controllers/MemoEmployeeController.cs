using MemoApp.Models.Dto;
using MemoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemoApp.Controllers
{
    public class MemoEmployeeController : Controller
    {
        //Access to the services
        private readonly DbService DbService;
        public UserService UserService;

        public MemoEmployeeController(DbService dbContext, UserService userService)
        {
            DbService = dbContext;
            UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Signing(int IdMemo)
        {
            var employee = UserService.GetActiveEmployee();
            ViewBag.Employee = employee;
            var memo = DbService.getDetailMemo(IdMemo).Result;
            var memoEmployee = DbService.getMemoEmployee(IdMemo, employee.EmployeeId).Result;

            MemoEmployeeDto memoEmployeeDto = new MemoEmployeeDto()
            { 
                Name = memo.Name,
                Description = memo.Description,
                CreatedBy = memo.CreatedBy,
                Signed = memoEmployee.Signed,
            };
            var model = memoEmployeeDto;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signing(string password)
        {
            ViewBag.Employee = UserService.GetActiveEmployee();
            //Verifier le mote de passe entrer si correcte indiquer succes et retourner menu principal
            //Si incorrecte indiquer erreur et View()
            return View(); 
        }
    }
}
