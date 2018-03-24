using System;
using car_heap.Controllers.Resources.UserResources;
using car_heap.Controllers.Resources.VehicleResources;

namespace car_heap.Controllers.Resources.OrderResources
{
    public class PlainOrderResource
    {
        public PlainUserResource User { get; set; }

        public PlainVehicleResource Vehicle { get; set; }

        public string Comment { get; set; }

        public KeyValuePairResource Status { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime DateExpired { get; set; }
    }
}