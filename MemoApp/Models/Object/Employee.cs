using System.ComponentModel.DataAnnotations;

namespace MemoApp.Models.Object
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }

        public ICollection<EmployeeJob> EmployeeJobs { get; set; }
        public ICollection<MemoEmployee> MemoEmployees { get; set; }
    }
}
