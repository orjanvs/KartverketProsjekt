using KartverketProsjekt.Models.DomainModels;

namespace KartverketProsjekt.Models.ViewModels
{
    public class ViewMapReportRequest
    {
        public int MapReportId { get; set; }
        public string Description { get; set; }
        public int MapLayerId { get; set; }
        public string GeoJsonString { get; set; }
        public int MapReportStatusId { get; set; }
        public DateTime SubmissionDate { get; set; }

        public string SubmitterId { get; set; } 
        public string? CaseHandlerId { get; set; }

        public MapReportStatusModel MapReportStatus { get; set; } // Navigation property for status
        public ICollection<AttachmentModel> Attachments { get; set; } // Navigation property for attachments

        public ApplicationUser Submitter { get; set; } // Navigation to Submitter (User)
        public ApplicationUser CaseHandler { get; set; } // Navigation to CaseHandler (User)
    }
}
