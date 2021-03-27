using System.ComponentModel.DataAnnotations;

namespace CustomerManager.API.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
