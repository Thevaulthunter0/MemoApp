namespace MemoApp.Models.Dto
{
    public class MemoAssignedDto
    {
        public int MemoId { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Signed { get; set; }
    }
}
