﻿namespace MemoApp.Models.Dto
{
    public class DetailMemoDto
    {
        public int MemoId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
