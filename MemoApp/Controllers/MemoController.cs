using MemoApp.Models.Object;
using MemoApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using MemoApp.Services;

namespace MemoApp.Controllers
{
    public class MemoController : Controller
    {
        //Access to the services
        private readonly DbService DbService;
        public UserService UserService;

        public MemoController(DbService dbContext, UserService userService)
        {
            DbService = dbContext;
            UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int IdMemo)
        {
            var memo = DbService.getDetailMemo(IdMemo);
            ViewBag.Employee = UserService.GetActiveEmployee(); 
            DetailMemoDto memoDto = new DetailMemoDto()
            {
                MemoId = memo.Result.MemoId,
                Name = memo.Result.Name,
                Description = memo.Result.Description,
                CreationDate = memo.Result.CreationDate,
                CreatedBy = memo.Result.CreatedBy,
                ModificationDate = memo.Result.ModificationDate,
                ModifiedBy = memo.Result.ModifiedBy,
            };
            var model = memoDto;
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int IdMemo)
        {
            var memo = DbService.getDetailMemo(IdMemo).Result;
            ViewBag.Employee = UserService.GetActiveEmployee();
            EditMemoDto Dto = new EditMemoDto()
            {
                MemoId = memo.MemoId,
                Name = memo.Name,
                Description = memo.Description,
                ModificationDate = memo.ModificationDate,
                ModifiedBy = memo.ModifiedBy,
            };
            return View(Dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMemoDto EditedMemo)
        {
            var data = DbService.getDetailMemo(EditedMemo.MemoId).Result;
            
            if(data != null)
            {
                data.Name = EditedMemo.Name;
                data.Description = EditedMemo.Description;
                data.ModificationDate = DateTime.Now;
                data.ModifiedBy = UserService.GetActiveEmployee().Name;
                DbService.SaveChanges();
                return RedirectToAction("Index", "Employee", new { IdEmployee = UserService.GetActiveEmployeeId()});
            }
            else
            {
                return View(EditedMemo);
            } 
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Jobs = DbService.getJobs().Result;
            ViewBag.Employee = UserService.GetActiveEmployee(); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemoDto CreateMemoDto) 
        {
            ViewBag.Jobs = DbService.getJobs().Result;
            ViewBag.Employee = UserService.GetActiveEmployee();
            int LastId = DbService.getLastMemo().Result.MemoId;
            Memo data = new Memo()
            {
                //MemoId = LastId + 1,
                Name = CreateMemoDto.Name,
                Description = CreateMemoDto.Description,
                CreationDate = DateTime.Now,
                CreatedBy = UserService.GetActiveEmployee().Name,
                ModificationDate = null,
                ModifiedBy = null
            };
            DbService.AddMemo(data);
            CreateMemoJob(data.MemoId, CreateMemoDto.JobsId);
            CreateMemoEmployee(data.MemoId, CreateMemoDto.JobsId);
            return RedirectToAction("Index", "Employee", new { IdEmployee = UserService.GetActiveEmployeeId() });
        }

        public void CreateMemoJob(int MemoId, List<int> JobsId)
        {
            foreach(var Id in JobsId)
            {
                DbService.CreateMemoJob(MemoId, Id);
            }
            DbService.SaveChanges();
        }

        public void CreateMemoEmployee(int MemoId, List<int> JobsId)
        {
            foreach(var Id in JobsId)
            {
                var EmployeesId = DbService.getEmployeesIdFromJobId(Id).Result;
                foreach(var EmployeeId in EmployeesId)
                {
                    DbService.CreateMemoEmployee(MemoId, EmployeeId);
                }
                DbService.SaveChanges();
            }
        }
    }
}
