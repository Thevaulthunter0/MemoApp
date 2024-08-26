using System.ComponentModel.DataAnnotations;

namespace MemoApp.Models.Object
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string? Name { get; set; }

        public ICollection<EmployeeJob> EmployeeJob { get; set; }
        public ICollection<MemoJob> MemoJobs { get; set; }
    }
}
