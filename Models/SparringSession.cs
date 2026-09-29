using MyMvcApp.Models.Enums;

namespace MyMvcApp.Models
{
    public class SparringSession
    {
        public int Id { get; set; }
        public string? SparringPrtnerName { get; set; }
        public BeltRank? SparringParatnerBelt {  get; set; }
        public string? SparringPartnerAcademy { get; set; }
        public SparringResult? SparringResult { get; set; }
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }
    }
}
