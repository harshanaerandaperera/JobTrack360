using JobTrack360.DataEF;
using JobTrack360.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTrack360.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        public JobApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobApplication> AddAsync(JobApplication app)
        {
            _context.JobApplications.Add(app);
            await _context.SaveChangesAsync();
            return app;
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync() => await _context.JobApplications.ToListAsync();
        
        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            return await _context.JobApplications.FindAsync(id);
        }

        public async Task<JobApplication?> UpdateAsync(JobApplication app)
        {
            var existing = await _context.JobApplications.FindAsync(app.Id);
            if (existing == null) return null;

            existing.Status = app.Status;
            existing.Notes = app.Notes;
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<JobApplication?> DeleteAsync(int id)
        {
            var existing = await _context.JobApplications.FindAsync(id);
            if (existing == null) return null;

            _context.JobApplications.Remove(existing);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
