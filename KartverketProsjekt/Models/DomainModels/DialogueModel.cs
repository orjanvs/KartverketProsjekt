namespace KartverketProsjekt.Models.DomainModels
{
    public class DialogueModel
    {
        public int DialogueId { get; set; } // PK
        public int MapReportId { get; set; } // FK
        public string SenderId { get; set; } // FK (Ref. User(UserId))
        public string RecipientId { get; set; } // FK (Ref. User(UserId))
        public string Message { get; set; }
        public  DateTime Timestamp{ get; set; }

        // Navigation properties
        public MapReportModel MapReport { get; set; } // Navigation to MapReport
        public ApplicationUser Sender { get; set; } // Navigation to User as Sender
        public ApplicationUser Recipient { get; set; } // Navigation to User as Recipient

    }
}
