using System.ComponentModel.DataAnnotations;

namespace car_heap.Core.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(12, MinimumLength = 12)]
        public string Phone { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}