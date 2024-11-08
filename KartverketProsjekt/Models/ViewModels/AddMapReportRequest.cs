using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace KartverketProsjekt.Models.ViewModels
{
    public class AddMapReportRequest
    {
        [Required]
        public string GeoJson { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MapLayerId { get; set; }

        public List<IFormFile> Attachments { get; set; } // Legg til dette feltet for vedleggsopplasting

    }
}
