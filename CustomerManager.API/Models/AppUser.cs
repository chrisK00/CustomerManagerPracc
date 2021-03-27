using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CustomerManager.API.Models
{
    public class AppUser : IdentityUser
    {    
        public string LookingFor { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
