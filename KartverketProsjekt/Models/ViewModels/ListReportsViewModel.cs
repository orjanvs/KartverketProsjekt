namespace KartverketProsjekt.Models.ViewModels;

/// <summary>
/// Represents a view model for displaying a list of map reports with summary details.
/// </summary>
public class ListReportsViewModel
{
    public int MapReportId { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string GeoJsonString { get; set; }
    public string MapLayerType { get; set; }
    public bool HasAttachments { get; set; }
    public string StatusDescription { get; set; }
    public string County { get; set; }
    public string Municipality { get; set; }
}
