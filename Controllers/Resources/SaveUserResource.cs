using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources
{
    public class SaveUserResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime DateRegistered { get; set; }

        public ContactResource Contact { get; set; }
    }
}