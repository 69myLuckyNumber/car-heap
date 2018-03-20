using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdated { get; set; }

        public int ModelId { get; set; }

        public string IdentityId { get; set; }

        public ICollection<int> Features { get; set; }

        public ICollection<OrderResource> Orders { get; set; }

        public SaveVehicleResource()
        {
            Features = new Collection<int>();
            Orders = new Collection<OrderResource>();
        }
    }
}