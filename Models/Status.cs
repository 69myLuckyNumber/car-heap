using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_heap.Models
{
    [Table("Statuses")]
    public class Status
    {
        public int StatusId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }   
    }
}