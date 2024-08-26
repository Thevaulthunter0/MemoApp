using System.ComponentModel.DataAnnotations;

namespace MemoApp.Models.Object
{
    public class Memo
    {
        [Key]
        public int MemoId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModifiedBy { get; set; }

        public ICollection<MemoEmployee> MemoEmployees { get; set; }
        public ICollection<MemoJob> MemoJobs { get; set; }
    }
}
