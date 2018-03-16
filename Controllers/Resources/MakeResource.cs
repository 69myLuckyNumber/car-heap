using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace car_heap.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<ModelResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }
    }
}