using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace car_heap.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateRegistered { get; set; }

        public ICollection<Vehicle> OfferedVehicles { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Contact Contact { get; set; }

        public ApplicationUser()
        {
            OfferedVehicles = new Collection<Vehicle>();
            Orders          = new Collection<Order>();
        }
    }
}