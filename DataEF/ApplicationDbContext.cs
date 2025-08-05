using JobTrack360.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTrack360.DataEF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<JobApplication> JobApplications { get; set; }
    }
  
}
