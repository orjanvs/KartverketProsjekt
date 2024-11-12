namespace KartverketProsjekt.Models.DomainModels
{
    /// <summary>
    /// Represents the status of a map report with a unique identifier and description.
    /// </summary>
    public class MapReportStatusModel
    {
        public int MapReportStatusId { get; set; } // Primary Key
        public string StatusDescription { get; set; }
    }
}