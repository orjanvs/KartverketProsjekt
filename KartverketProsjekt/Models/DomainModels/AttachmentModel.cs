namespace KartverketProsjekt.Models.DomainModels
{
    /// <summary>
    /// Represents an attachment associated with a map report.
    /// </summary>
    public class AttachmentModel
    {
        public int AttachmentId { get; set; } // Primary key
        public int MapReportId { get; set; } // Foreign key (ref. MapReport(MapReportId))
        public string? FilePath { get; set; } // Proof of concept; no actual function  

        // Navigation property
        public MapReportModel MapReport { get; set; } // Navigation to MapReport
    }
}