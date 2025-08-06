using JobTrack360.Domain.Entities;

namespace JobTrack360.Application.Interfaces
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