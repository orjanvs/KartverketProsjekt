namespace KartverketProsjekt.Models.ViewModels
{
    public class ViewMapReportRequest
    {
        public int MapReportId { get; set; }
        public string Description { get; set; }
        public int MapLayerId { get; set; }
        public string GeoJsonString { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}
