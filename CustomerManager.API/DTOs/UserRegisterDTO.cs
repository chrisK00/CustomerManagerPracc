using System.ComponentModel.DataAnnotations;

namespace CustomerManager.API.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}