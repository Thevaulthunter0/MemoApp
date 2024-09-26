namespace MemoApp.Models.Dto
{
    public class MemoEmployeeDto
    {
        public int? MemoId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public bool Signed { get; set; }
    }
}
