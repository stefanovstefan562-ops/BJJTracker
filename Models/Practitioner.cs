using MyMvcApp.Models.Enums;

namespace MyMvcApp.Models
{
    public class Practitioner
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int? AcademyId { get; set; }
        public Academy? Academy { get; set; }
        public BeltRank CurrentBelt { get; set; }

    }
}
