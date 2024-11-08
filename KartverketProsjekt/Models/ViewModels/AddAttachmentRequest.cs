namespace KartverketProsjekt.Models.ViewModels
{
    public class AddAttachmentRequest
    {
        public int AttachmentId { get; set; }
        public int MapReportId { get; set; }
        public string? FilePath { get; set; }
    }
}
