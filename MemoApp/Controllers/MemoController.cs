using MemoApp.Models.Object;
using MemoApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MemoApp.Controllers
{
    public class MemoController : Controller
    {
        //Access to the services
        private readonly DbService DbService;

        public MemoController(DbService dbContext)
        {
            DbService = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int IdMemo)
        {
            var memo = DbService.getDetailMemo(IdMemo);
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
            return View(memoDto);
        }
    }
}
