using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KartverketProsjekt.Models.ViewModels
{
    public class ViewMapReportRequest
    {
        public int MapReportId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MapLayerId { get; set; }
        public string GeoJsonString { get; set; }
        public int MapReportStatusId { get; set; }
        public DateTime SubmissionDate { get; set; }

        public string SubmitterId { get; set; } 
        public string? CaseHandlerId { get; set; }

        public ICollection<AttachmentModel> Attachments { get; set; } // Navigation property for attachments
        public MapReportStatusModel MapReportStatus { get; set; } // Navigation property for status
        public MapLayerModel MapLayer { get; set; } // Navigation property for map layer

        public ApplicationUser Submitter { get; set; } // Navigation to Submitter (User)
        public ApplicationUser CaseHandler { get; set; } // Navigation to CaseHandler (User)

        public List<SelectListItem> CaseHandlers { get; set; } // Add this property

    }
}
