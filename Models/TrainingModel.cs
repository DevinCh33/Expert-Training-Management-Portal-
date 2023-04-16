namespace ETMP.Models
{
    public class TrainingModel
    {
        public int Id { get; set; }
        public string? TrainingName { get; set; }
        public int TrainingPrice { get; set; }
        public string? TrainingItinerary { get; set; }
        public string? TrainingVenue { get; set; }
        public string? TrainingCategory { get; set; }
        public bool Availability { get; set; }
    }
}
