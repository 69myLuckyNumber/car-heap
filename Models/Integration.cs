using System.ComponentModel.DataAnnotations.Schema;

namespace car_heap.Models
{
    [Table("Integrations")]
    public class Integration
    {
        public int FeatureId { get; set; }

        public int VehicleId { get; set; }

        public Feature Feature { get; set; }

        public Vehicle Vehicle { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }
    }
}