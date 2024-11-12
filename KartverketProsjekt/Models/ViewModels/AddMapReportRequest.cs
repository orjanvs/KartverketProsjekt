using System.ComponentModel.DataAnnotations;

namespace KartverketProsjekt.Models.ViewModels
{
    /// <summary>
    /// Represents a request to add a new map report, including details such as GeoJson shape, title, description, map layer, and optional attachments.
    /// </summary>
    public class AddMapReportRequest
    {
        [Required(ErrorMessage = "A GeoJson shape is required. Please draw a shape on the map.")]
        public string? GeoJson { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int MapLayerId { get; set; }

        public string? County { get; set; }
        
        public string? Municipality { get; set; }

        public List<IFormFile>? Attachments { get; set; } // Field for uploading attachments

    }
}
