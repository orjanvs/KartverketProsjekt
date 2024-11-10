using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;

namespace KartverketProsjekt.Models.DomainModels
{
    public class MapReportModel
    {
        public int MapReportId { get; set; } // Primary key
        [Required]
        public string? SubmitterId { get; set; } // FK (ref. User(UserId))     
        public string? CaseHandlerId { get; set; } // FK (ref. User(UserId))
        [Required]                                         
        public int MapLayerId { get; set; } // FK (ref. MapLayer(MapLayerId))
        [Required]
        public int MapReportStatusId { get; set; } // FK (ref. MapReportStatus(MapReportStatusId))
        [Required]
        public string? Title { get; set; }
        [Required]
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
        public MapLayerModel? MapLayer { get; set; } // Navigation to MapLayer
        public MapReportStatusModel? MapReportStatus { get; set; } // Navigation to MapReportStatus
        public ICollection<AttachmentModel>? Attachments { get; set; } // Collection for Attachments
    }
}
