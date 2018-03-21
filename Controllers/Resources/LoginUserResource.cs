using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources
{
    public class LoginUserResource
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}