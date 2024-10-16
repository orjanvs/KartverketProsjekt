﻿namespace KartverketProsjekt.Models.ViewModels
{
    public class ViewMapReportRequest
    {
        public int MapReportId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string GeoJson { get; set; }
        public string Attachment { get; set; }
        public string CaseStatus { get; set; }
        public DateOnly SubmissionDate { get; set; }
    }
}
