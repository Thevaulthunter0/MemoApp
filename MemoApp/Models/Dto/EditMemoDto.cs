namespace MemoApp.Models.Dto
{
    public class EditMemoDto
    {
        public int MemoId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
