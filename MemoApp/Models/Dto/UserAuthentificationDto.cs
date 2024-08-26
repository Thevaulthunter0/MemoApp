namespace MemoApp.Models.Dto
{
    using MemoApp.Models.Object;
    using System.ComponentModel.DataAnnotations;

    public class UserAuthentificationDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
