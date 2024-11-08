namespace KartverketProsjekt.Models.ViewModels;

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

}
