using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources.VehicleResources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public bool IsRegistered { get; set; }
        
        [Required(ErrorMessage="Model is not specified")]
        public int ModelId { get; set; }

        [Required]
        public string IdentityId { get; set; }

        public ICollection<int> Features { get; set; }

        public SaveVehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}