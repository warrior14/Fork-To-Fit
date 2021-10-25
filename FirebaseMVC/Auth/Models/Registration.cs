using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ForkToFit.Auth.Models
{
    public class Registration
    {

        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }


        public DateTime DateCreated { get; set; }

    }
}

// you can have more properties in a model compared to your database

