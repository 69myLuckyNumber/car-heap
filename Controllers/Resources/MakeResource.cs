using System.Collections.Generic;
using System.Collections.ObjectModel;
using car_heap.Models;

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