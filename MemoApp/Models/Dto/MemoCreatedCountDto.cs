namespace MemoApp.Models.Dto
{
    public class MemoCreatedCountDto
    {
        public int MemoId { get; set; }
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModifiedBy { get; set; }

        public int TotalAssigned { get; set; }
        public int ReadAssigned { get; set; }
    }
}
