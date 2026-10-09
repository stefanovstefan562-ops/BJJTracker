namespace MyMvcApp.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int TrainingSessionId { get; set; }
        public  TrainingSession? TrainingSession {  get; set; }
        public int PractitionerId { get; set; }
        public  Practitioner? Practitioner {  get; set; }
    }
}
