using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources.UserResources
{
    public class ContactResource
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string Email { get; set; }
       
        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}