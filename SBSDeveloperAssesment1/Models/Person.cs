using System;
using System.ComponentModel.DataAnnotations;

namespace SBSDeveloperAssesment1.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Password { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
