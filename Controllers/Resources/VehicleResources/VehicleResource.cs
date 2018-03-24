using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using car_heap.Controllers.Resources.OrderResources;
using car_heap.Controllers.Resources.UserResources;

namespace  car_heap.Controllers.Resources.VehicleResources
{
    public class VehicleResource 
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdated { get; set; }

        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }

        public PlainUserResource User { get; set; }

        public ICollection<FeatureResource> Features { get; set; }

        public ICollection<PlainOrderResource> Orders { get; set; }

        public VehicleResource()
        {
            Features = new Collection<FeatureResource>();
            Orders = new Collection<PlainOrderResource>();
        }
    }
}