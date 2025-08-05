namespace JobTrack360.Models
{
    public class JobApplication
    {
        public int Id { get; set; } // Unique identifier for the job application
        public string CompanyName { get; set; } // Name of the company where the application was submitted
        public string Position { get; set; }    // Position applied for
        public string Notes { get; set; } // Additional notes about the application
        public string Status { get; set; } // Current status of the application (Interview/Offer/Rejected)    
        public DateTime DateApplied { get; set; } // Date when the application was submitted
    }
}
