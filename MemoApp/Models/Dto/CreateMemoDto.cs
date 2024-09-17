namespace MemoApp.Models.Dto
{
    public class CreateMemoDto
    {
        public int MemoId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreatedBy { get; set; }
        public List<int>? JobsId { get; set; }
    }
}
