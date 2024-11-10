using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace KartverketProsjekt.Models.ViewModels
{
    public class AddMapReportRequest
    {
        [Required(ErrorMessage = "A GeoJson shape is required. Please draw a shape on the map.")]
        public string GeoJson { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MapLayerId { get; set; }

        public string? County { get; set; }
        
        public string? Municipality { get; set; }

        public List<IFormFile>? Attachments { get; set; } // Legg til dette feltet for vedleggsopplasting

    }
}
