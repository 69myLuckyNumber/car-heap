using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace car_heap.Controllers.Resources.UserResources
{
    public class SaveUserResource
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(32, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string FirstName { get; set; }

        [StringLength(32)]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
    }
}