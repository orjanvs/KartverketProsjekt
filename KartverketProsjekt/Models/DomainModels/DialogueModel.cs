namespace KartverketProsjekt.Models.DomainModels
{
    /// <summary>
    /// Represents a dialogue entry related to a map report, including sender, recipient, message content, and timestamp.
    /// </summary>
    public class DialogueModel
    {
        public int DialogueId { get; set; } // Primary key
        public int MapReportId { get; set; } // Foreign key
        public string SenderId { get; set; } // Foreign key (Ref. User(UserId))
        public string RecipientId { get; set; } // Foreign key (Ref. User(UserId))
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation properties
        public MapReportModel MapReport { get; set; } // Navigation to MapReport
        public ApplicationUser Sender { get; set; } // Navigation to User as Sender
        public ApplicationUser Recipient { get; set; } // Navigation to User as Recipient
    }
}