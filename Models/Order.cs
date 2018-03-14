using System;

namespace car_heap.Models
{
    public class Order
    {
        public int UserId { get; set; }

        public int VehicleId { get; set; }

        public User User { get; set; }

        public Vehicle Vehicle { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string Comment { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime DateExpired { get; set; }
    }
}