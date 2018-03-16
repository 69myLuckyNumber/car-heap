using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace car_heap.Controllers.Resources
{
    public class MakeResource
    {
        public int MakeId { get; set; }
        
        public string Name { get; set; }

        public ICollection<ModelResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }
    }
}