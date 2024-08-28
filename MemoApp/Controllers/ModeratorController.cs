using MemoApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MemoApp.Controllers
{
    public class ModeratorController : Controller
    {
        //Access to the services
        private readonly DbService DbService;

        public ModeratorController(DbService dbContext)
        {
            DbService = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            //Fetch the data
            var DbMemo = DbService.getMemos();
            //Transfor the data into the Dto
            List<FullMemoDto> MemosDto = new List<FullMemoDto>();
            foreach (var mem in await DbMemo)
            {
                FullMemoDto memo = new FullMemoDto();
                {
                    memo.MemoId = mem.MemoId;
                    memo.Name = mem.Name;
                    memo.Description = mem.Description;
                    memo.CreationDate = mem.CreationDate;
                    memo.CreatedBy = mem.CreatedBy;
                    memo.ModificationDate = mem.ModificationDate;
                    memo.ModifiedBy = mem.ModifiedBy;
                }
                MemosDto.Add(memo);
            }
            //Set the model of the partial view.
            var model = MemosDto;
            return View(model);
        }
    }
}
