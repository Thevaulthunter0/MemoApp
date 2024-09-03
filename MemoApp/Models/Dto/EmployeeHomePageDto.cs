using MemoApp.Models.Object;

namespace MemoApp.Models.Dto
{
    public class EmployeeHomePageDto
    { 
        public string? Name { get; set; }
        public List<Job>? Jobs { get; set; }
        public List<MemoAssignedDto>? MemosAssigned { get; set; }
        public List<MemoCreatedCountDto>? MemosCreatedCount { get; set; } 
    }
}
