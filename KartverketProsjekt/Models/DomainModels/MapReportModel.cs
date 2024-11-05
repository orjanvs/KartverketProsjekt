namespace KartverketProsjekt.Models.DomainModels
{
    public class MapReportModel
    {
        public int MapReportId { get; set; } // Primary key
        public string SubmitterId { get; set; } // FK (ref. User(UserId))     
        public string? CaseHandlerId { get; set; } // FK (ref. User(UserId))  
        public int MapLayerId { get; set; } // FK (ref. MapLayer(MapLayerId))
        public int MapReportStatusId { get; set; } // FK (ref. MapReportStatus(MapReportStatusId))
        public string Title { get; set; }
        public string Description { get; set; }
        public string GeoJsonString { get; set; }
        public DateTime SubmissionDate { get; set; }

        // Navigation properties
        public ApplicationUser Submitter { get; set; } // Navigation to Submitter (User)
        public ApplicationUser CaseHandler { get; set; } // Navigation to CaseHandler (User)
        public MapLayerModel MapLayer { get; set; } // Navigation to MapLayer
        public MapReportStatusModel MapReportStatus { get; set; } // Navigation to MapReportStatus
        public ICollection<AttachmentModel> Attachments { get; set; } // Collection for Attachments
    }
}
