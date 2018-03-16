using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_heap.Core.Models
{
    [Table("Models")]
    public class Model
    {
        public int ModelId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int MakeId { get; set; }
        public Make Make { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public Model()
        {
            Vehicles = new Collection<Vehicle>();
        }
    }
}