using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MemoApp.Models.Object
{
    public class MemoJob
    {
        public int MemoId { get; set; }
        public int JobId { get; set; }

        [ForeignKey("MemoId")]
        public Memo Memo { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}
