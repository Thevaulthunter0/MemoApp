using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MemoApp.Models.Object
{
    public class EmployeeJob
    {
        public int JobId { get; set; } 
        public int EmployeeId { get; set; }

        [ForeignKey("JobId")]
        public Job Job { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
