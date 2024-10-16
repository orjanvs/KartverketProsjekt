﻿namespace KartverketProsjekt.Models.DomainModels
{
    public class MapReportModel
    {
        public int MapReportId { get; set; } // Primary key
        public string Description { get; set; }
        public string Category { get; set; }
        public string GeoJson { get; set; }
        public string Attachment { get; set; }
        public string CaseStatus { get; set; }
        public DateOnly SubmissionDate { get; set; }
    }
}
