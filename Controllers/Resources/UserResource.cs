using System;

namespace car_heap.Controllers.Resources
{
    public class UserResource
    {
        public int Id { get; set; }

        public string Username { get; set; }
        
        public DateTime DateRegistered { get; set; }

        public ContactResource Contact { get; set; }
    }
}