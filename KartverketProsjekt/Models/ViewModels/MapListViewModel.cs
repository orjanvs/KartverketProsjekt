namespace KartverketProsjekt.Models.ViewModels
{
    /// <summary>
    /// Represents a view model for displaying a list of map data with report ID, GeoJson data, and layer ID.
    /// </summary>
    public class MapListViewModel
    {
        public int MapReportId { get; set; }
        public string GeoJsonString { get; set; }
        public int MapLayerId { get; set; }
    }
}
