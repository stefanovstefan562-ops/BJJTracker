using Microsoft.AspNetCore.Identity;

namespace BjjTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? AcademyId { get; set; }
        public Academy? Academy { get; set; }
        public int? PractitionerId { get; set; }
        public Practitioner? Practitioner { get; set; }
    }
}
