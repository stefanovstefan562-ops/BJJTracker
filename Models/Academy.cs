namespace MyMvcApp.Models
{
    public class Academy
    {
        public int Id { get; set; }
        public required string AcademyName { get; set; }
        public string? AcademyAddress { get; set; }
        public string? AcademyDescription { get; set; }
        public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
    }
}
