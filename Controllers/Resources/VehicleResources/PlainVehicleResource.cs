using System;
using System.Collections.Generic;

namespace car_heap.Controllers.Resources.VehicleResources
{
    public class PlainVehicleResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdated { get; set; }

        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }
    }
}