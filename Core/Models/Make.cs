using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace car_heap.Core.Models
{
    public class Make
    {
        public int MakeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }

        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}