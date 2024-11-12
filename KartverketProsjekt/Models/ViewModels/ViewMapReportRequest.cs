using Microsoft.AspNetCore.Mvc.Rendering;

namespace KartverketProsjekt.Models.ViewModels
{
    /// <summary>
    /// Represents the data required to view and manage details of a specific map report, 
    /// including report information, status, submitter, case handler, attachments, 
    /// and available case handlers for assignment.
    /// </summary>
    public class ViewMapReportRequest
    {
        public int MapReportId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int MapLayerId { get; set; }
        public string? MapLayerType { get; set; }
        public string? GeoJsonString { get; set; }
        public int MapReportStatusId { get; set; }
        public string? StatusDescription { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string? County { get; set; }
        public string? Municipality { get; set; }


        public string? SubmitterId { get; set; } 
        public string? SubmitterName { get; set; }

        public string? CaseHandlerId { get; set; }
        public string? CaseHandlerName { get; set; }



        public ICollection<AddAttachmentRequest>? Attachments { get; set; }

        public List<SelectListItem>? AvailableCaseHandlers { get; set; }


    }
}
