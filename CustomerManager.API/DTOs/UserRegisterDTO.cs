using System.ComponentModel.DataAnnotations;

namespace CustomerManager.API.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string Username { get; set; }
    }
}
