using JobTrack360.Models;

namespace JobTrack360.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllAsync();
        Task<JobApplication?> GetByIdAsync(int id);
        Task<JobApplication> AddAsync(JobApplication app);
        Task<JobApplication?> UpdateAsync(JobApplication app);
        Task<JobApplication?> DeleteAsync(int id); 
    }
}
