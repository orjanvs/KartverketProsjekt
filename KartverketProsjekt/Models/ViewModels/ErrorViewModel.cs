namespace KartverketProsjekt.Models.ViewModels
{
    /// <summary>
    /// Represents an error view model containing information about a specific request.
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
