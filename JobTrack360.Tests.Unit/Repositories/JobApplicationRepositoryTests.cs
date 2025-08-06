using JobTrack360.Domain.Entities;
using JobTrack360.Domain.Enums;
using JobTrack360.Infrastructure.Data;
using JobTrack360.Repositories;
using Microsoft.EntityFrameworkCore;


namespace JobTrack360.Tests.Unit.Repositories
{
    public class JobApplicationRepositoryTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_JobApplication()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new JobApplicationRepository(context);
            var job = new JobApplication
            {
                CompanyName = "Test Company",
                Position = "Developer",
                Status = JobStatus.Applied,
            };

            // Act
            var result = await repo.AddAsync(job);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Company", result.CompanyName);
            Assert.Single(await context.JobApplications.ToListAsync());
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Correct_JobApplication()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var job = new JobApplication { CompanyName = "Company A", Position = "Tester", Status = JobStatus.Interview };
            context.JobApplications.Add(job);
            await context.SaveChangesAsync();

            var repo = new JobApplicationRepository(context);

            // Act
            var result = await repo.GetByIdAsync(job.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Company A", result.CompanyName);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Status_And_Notes_When_JobApplication_Exists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repo = new JobApplicationRepository(context);

            var job = new JobApplication
            {
                CompanyName = "Dev Co",
                Position = "Junior Dev",
                Status = JobStatus.Applied,
                Notes = "Initial application"
            };

            context.JobApplications.Add(job);
            await context.SaveChangesAsync();

            var updated = new JobApplication
            {
                Id = job.Id,
                Status = JobStatus.Interview,
                Notes = "Phone interview scheduled"
            };

            // Act
            var result = await repo.UpdateAsync(updated);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(JobStatus.Interview, result.Status);
            Assert.Equal("Phone interview scheduled", result.Notes);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_JobApplication()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var job = new JobApplication { CompanyName = "Company X", Position = "QA", Status = JobStatus.Rejected };
            context.JobApplications.Add(job);
            await context.SaveChangesAsync();

            var repo = new JobApplicationRepository(context);

            // Act
            var deleted = await repo.DeleteAsync(job.Id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(job.Id,deleted.Id);
            
        }
    }
}
