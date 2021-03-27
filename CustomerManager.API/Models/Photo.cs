using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManager.API.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
