using System;

namespace car_heap.Controllers.Resources.UserResources
{
    public class PlainUserResource
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public DateTime DateRegistered { get; set; }

        public ContactResource Contact { get; set; }
    }
}