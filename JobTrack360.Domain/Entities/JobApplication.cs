using JobTrack360.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Company Name is Required")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Position applied for
        /// </summary>
        [Required(ErrorMessage = "Position is Required")]
        public string Position { get; set; }

        /// <summary>
        /// Additional notes about the application
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Current status of the application (Applied/Interview/Offer/Rejected) 
        /// </summary>
        [Required]
        [EnumDataType(typeof(JobStatus))]
        public JobStatus Status { get; set; }

        /// <summary>
        /// Date when the application was submitted
        /// </summary>
        public DateTime DateApplied { get; set; }   = DateTime.UtcNow; // Set the date when the application was submitted
    }
}
