using MemoApp.Models.Object;
using MemoApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using MemoApp.Services;
using System.Drawing.Printing;

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
            var memo = await DbService.getDetailMemo(IdMemo);
            ViewBag.Employee = UserService.GetActiveEmployee(); 
            DetailMemoDto memoDto = new DetailMemoDto()
            {
                MemoId = memo.MemoId,
                Name = memo.Name,
                Description = memo.Description,
                CreationDate = memo.CreationDate,
                CreatedBy = memo.CreatedBy,
                ModificationDate = memo.ModificationDate,
                ModifiedBy = memo.ModifiedBy,
            };
            var model = memoDto;
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int IdMemo)
        {
            var memo = await DbService.getDetailMemo(IdMemo);
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
            var data = await DbService.getDetailMemo(EditedMemo.MemoId);
            
            if(data != null)
            {
                data.Name = EditedMemo.Name;
                data.Description = EditedMemo.Description;
                data.ModificationDate = DateTime.Now;
                data.ModifiedBy = UserService.GetActiveEmployee().Name;
                await DbService.SaveChanges();
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
            ViewBag.Jobs =  await DbService.getJobs();
            ViewBag.Employee = UserService.GetActiveEmployee(); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemoDto CreateMemoDto, IFormFile file) 
        {
            ViewBag.Jobs = await DbService.getJobs();
            ViewBag.Employee = UserService.GetActiveEmployee();
            var lastMemo = await DbService.getLastMemo();
            int LastId = lastMemo.MemoId;
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
            await DbService.AddMemo(data);

            await DbService.SaveChanges();

            await CreateMemoJob(data.MemoId, CreateMemoDto.JobsId);

            if (file != null && file.ContentType == "application/pdf")
            {
                await UploadFile(file, data.Name, CreateMemoDto.JobsId);
                return RedirectToAction("Index", "Employee", new { IdEmployee = UserService.GetActiveEmployeeId() });
            }

            return View(CreateMemoDto);
        }

        public async Task CreateMemoJob(int MemoId, List<int> JobsId)
        {
            Console.WriteLine("******************Before MemoJobs creation**************************");
            foreach(var Id in JobsId)
            {
                await DbService.CreateMemoJob(MemoId, Id);
            }
            Console.WriteLine("*******************MemosJobs Created***************************");
            await DbService.SaveChanges();
            Console.WriteLine("************************MemoJobs save**************************");
            await CreateMemoEmployee(MemoId, JobsId);
        }

        public async Task CreateMemoEmployee(int MemoId, List<int> JobsId)
        {
            Console.WriteLine("************************before MemoEmploye creation**************************");
            foreach (var Id in JobsId)
            {
                var EmployeesId = await DbService.getEmployeesIdFromJobId(Id);
                foreach(var EmployeeId in EmployeesId)
                {
                    await DbService.CreateMemoEmployee(MemoId, EmployeeId);
                }
            }
            Console.WriteLine("************************after MemoEmploye creation**************************");
            await DbService.SaveChanges();
            Console.WriteLine("************************after MemoEmploye save**************************");

        }

        public async Task UploadFile(IFormFile file, string NameMemo, List<int> JobId)
        {
            string MainDirectory = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "UploadedFile");
            //If main directory doesnt exist create it
            if(!Directory.Exists(MainDirectory))
            {
                Directory.CreateDirectory(MainDirectory);
            }
            //If the directory for each jobId doesnt exist create it
            //Change the name of the file for the memo name and add the file in each directory.
            foreach (var id in JobId)
            {
                string JobDirectory = Path.Combine(MainDirectory, id.ToString());
                if(!Directory.Exists(JobDirectory))
                {
                    Directory.CreateDirectory(JobDirectory);
                }
                var FilePath = Path.Combine(JobDirectory, NameMemo);
                //Upload it via stream
                using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                {
                   
                    await file.CopyToAsync(stream);
                }
            }
        }
    }
}
