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
                MemoId = IdMemo,
                Name = memo.Name,
                Description = memo.Description,
                CreatedBy = memo.CreatedBy,
                Signed = memoEmployee.Signed,
            };
            var model = memoEmployeeDto;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signing(MemoEmployeeDto model, string password)
        {
            var employee = UserService.GetActiveEmployee();
            ViewBag.Employee = employee;
            //Verifier le mot de passe entrer si correcte indiquer succes et retourner menu principal
            //Si incorrecte indiquer erreur et View()
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Passwword cannot be empty.");
                return View(model);
            }
            else
            {
                var user = DbService.GetUser(employee.EmployeeId);
                if (password == user.Result.Password)
                {
                    DbService.SignMemoEmployee(model.MemoId, employee.EmployeeId);
                    return RedirectToAction("Index", "Employee", new { IdEmployee = employee.EmployeeId });
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect password, please try again.");
                    return View(model);
                }
            }
        }
    }
}
