using System;
using System.ComponentModel.DataAnnotations;
using car_heap.Core.Models;

namespace car_heap.Controllers.Resources.OrderResources
{
    public class SaveOrderResource
    {
        [Required]
        public string IdentityId { get; set; }

        [Required]
        public int? VehicleId { get; set; }

        public string Comment { get; set; }

        [Required]
        public int? StatusId { get; set; }
    }
}