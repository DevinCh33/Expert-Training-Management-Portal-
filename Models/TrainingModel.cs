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
        public string? TrainingDescription { get; set; }
        public string? TrainingImgURL { get; set; }
        public DateTime TrainingStartDateTime { get; set; }
        public DateTime TrainingEndDateTime { get; set; }
        public TimeSpan Duration => TrainingEndDateTime - TrainingStartDateTime;
		public string? TrainingMaterialFilePath { get; set; }
	}
}




