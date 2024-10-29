﻿using KartverketProsjekt.Models.DomainModels;

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


        public MapReportStatusModel MapReportStatus { get; set; } // Navigation property for status
    }
}
