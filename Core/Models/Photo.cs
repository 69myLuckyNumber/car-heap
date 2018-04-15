using System.ComponentModel.DataAnnotations;

namespace car_heap.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}