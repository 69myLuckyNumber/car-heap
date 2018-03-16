using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace car_heap.Core.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime DateRegistered { get; set; }

        public ICollection<Vehicle> OfferedVehicles { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Contact Contact { get; set; }

        public User()
        {
            OfferedVehicles = new Collection<Vehicle>();
            Orders          = new Collection<Order>();
        }
    }
}