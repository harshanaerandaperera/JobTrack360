namespace JobTrack360.Domain.Entities
{
    public class JobApplication
    {
        /// <summary>
        /// Unique identifier for the job application
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the company where the application was submitted
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Position applied for
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Additional notes about the application
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Current status of the application Ex. (Interview/Offer/Rejected) 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date when the application was submitted
        /// </summary>
        public DateTime DateApplied { get; set; }
    }
}
