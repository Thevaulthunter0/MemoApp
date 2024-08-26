using MemoApp.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace MemoApp.Models.Object
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public static async Task<int?> Authenticate(List<User> users, UserAuthentificationDto model)
        {
            // Check if the model is null or invalid
            if (model == null )
            {
                return null;
            }

            // Find the user that matches the provided credentials
            var user = users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            // If user is found, return the user's ID
            if (user != null)
            {
                return user.UserId;
            }

            // If no matching user is found, return null
            return null;
        }
    }
}
