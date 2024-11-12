namespace KartverketProsjekt.Models.ViewModels
{
    /// <summary>
    /// Represents a request to add an attachment to a map report, including attachment ID, report ID, and file path.
    /// </summary>
    public class AddAttachmentRequest
    {
        public int AttachmentId { get; set; }
        public int MapReportId { get; set; }
        public string? FilePath { get; set; }
    }
}
