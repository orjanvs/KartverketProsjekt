using System.ComponentModel.DataAnnotations;

namespace KartverketProsjekt.Models.DomainModels
{
    /// <summary>
    /// Represents a report on a map layer with related data, including submitter and case handler details, status, and attachments.
    /// </summary>
    public class MapReportModel
    {
        public int MapReportId { get; set; } // Primary key
        public string? SubmitterId { get; set; } // Foreign key (ref. User(UserId))     
        public string? CaseHandlerId { get; set; } // Foreign key (ref. User(UserId))
        [Required]
        public int MapLayerId { get; set; } // Foreign key (ref. MapLayer(MapLayerId))
        [Required]
        public int MapReportStatusId { get; set; } // Foreign key (ref. MapReportStatus(MapReportStatusId))
        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string? Title { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "{0} length must be below 300 characters")]
        public string? Description { get; set; }
        [Required]
        public string? GeoJsonString { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        public string? County { get; set; }
        public string? Municipality { get; set; }


        // Navigation properties
        public ApplicationUser? Submitter { get; set; } // Navigation to Submitter (User)
        public ApplicationUser? CaseHandler { get; set; } // Navigation to CaseHandler (User)
        [Required]
        public MapLayerModel? MapLayer { get; set; } // Navigation to MapLayer
        [Required]
        public MapReportStatusModel? MapReportStatus { get; set; } // Navigation to MapReportStatus
        public ICollection<AttachmentModel>? Attachments { get; set; } // Collection for Attachments
    }
}
