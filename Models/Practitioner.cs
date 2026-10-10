using MyMvcApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Practitioner
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The First name is mandatory!!!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The First name must be atleast 2 symbols!!!")]
        [RegularExpression(@"^[A-ZÀ-ÖØ-ÞА-Я][a-zA-ZÀ-ÖØ-öø-ÿА-Яа-я' -]*$", ErrorMessage = "First name must start with a capital letter and contain only letters, spaces, hyphens or apostrophes!!!")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "The Last name is mandatory!!!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Last name must be atleast 2 symbols!!!")]
        [RegularExpression(@"^[A-ZÀ-ÖØ-ÞА-Я][a-zA-ZÀ-ÖØ-öø-ÿА-Яа-я' -]*$", ErrorMessage = "Last name must start with a capital letter and contain only letters, spaces, hyphens or apostrophes!!!")]
        public required string LastName { get; set; }
        public int? AcademyId { get; set; }
        public Academy? Academy { get; set; }
        public BeltRank CurrentBelt { get; set; }

    }
}
