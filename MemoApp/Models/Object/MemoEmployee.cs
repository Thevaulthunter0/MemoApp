using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MemoApp.Models.Object
{
    public class MemoEmployee
    {
        public int MemoId { get; set; }
        public int EmployeeId { get; set; }
        public bool Signed { get; set; }

        [ForeignKey("MemoId")]
        public Memo Memo { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
