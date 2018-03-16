using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources
{
    public class ContactResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(12, MinimumLength = 12)]
        public string Phone { get; set; }
    }
}