using MyMvcApp.Models.Enums;

namespace MyMvcApp.Models
{
    public class Technique
    {
        public int Id { get; set; }
        public required string TechniqueName { get; set; }
        public required TechniquePosition TechniquePosition { get; set; }
        public string? TechniqueDescription { get; set; }
        public string? TechniqueVideo { get; set; }
    }
}
