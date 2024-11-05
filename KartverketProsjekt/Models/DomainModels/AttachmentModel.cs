namespace KartverketProsjekt.Models.DomainModels
{
    public class AttachmentModel
    {
        public int AttachmentId { get; set; } // Primary Key
        public int MapReportId { get; set; } // Foreign Key (ref. MapReport(MapReportId))
        public string? FilePath { get; set; } // Proof of concept; no actual function  

        // Navigation property
        public MapReportModel MapReport { get; set; } // Navigation to MapReport
    }
}
