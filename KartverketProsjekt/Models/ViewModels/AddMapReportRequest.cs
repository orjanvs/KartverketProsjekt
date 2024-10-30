using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KartverketProsjekt.Models.ViewModels
{
    public class AddMapReportRequest
    {
        [Required]
        public string GeoJson { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MapLayerId{ get; set; }

    }
}
