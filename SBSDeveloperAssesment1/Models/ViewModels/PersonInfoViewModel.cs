using System.ComponentModel.DataAnnotations;

namespace SBSDeveloperAssesment1.Models.ViewModels
{
    public class PersonInfoViewModel
    {
        [Required]
        public Person Person { get; set; }

        [Required]
        public Info Info { get; set; }
    }
}
