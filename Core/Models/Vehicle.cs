using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_heap.Core.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdated { get; set; }

        public ICollection<Integration> Features { get; set; }

        public ICollection<Order> Orders { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public string IdentityId { get; set; }
        public ApplicationUser Identity { get; set; }

        public Vehicle()
        {
            Features = new Collection<Integration>();
            Orders = new Collection<Order>();
        }
    }
}