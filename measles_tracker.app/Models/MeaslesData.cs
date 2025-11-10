namespace measles_tracker.app.Models
{
    public class MeaslesData
    {
        public string location_name { get; set; }
        public string location_id { get; set; }
        public string location_type { get; set; }
        public DateTime? date { get; set; }
        public string outcome_type { get; set; }
        public double? value { get; set; }
    }
}
