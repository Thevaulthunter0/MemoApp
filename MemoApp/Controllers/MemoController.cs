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


    }
}
