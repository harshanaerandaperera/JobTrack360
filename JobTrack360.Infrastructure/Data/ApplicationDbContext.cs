using JobTrack360.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTrack360.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<JobApplication> JobApplications { get; set; }
    }
  
}