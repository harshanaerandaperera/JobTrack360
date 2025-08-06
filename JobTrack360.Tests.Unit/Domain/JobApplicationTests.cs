using JobTrack360.Domain.Entities;
using JobTrack360.Domain.Enums;

namespace JobTrack360.Tests.Unit.Domain
{
    public class JobApplicationTests
    {
       
        [Fact]
        public void Can_Set_All_Properties()
        {
            // Arrange
            var job = new JobApplication
            {
                Id = 1,
                CompanyName = "OpenAI",
                Position = "Developer Advocate",
                Status = JobStatus.Interview,
                DateApplied = new DateTime(2025, 8, 5)
            };

            // Assert
            Assert.Equal(1, job.Id);
            Assert.Equal("OpenAI", job.CompanyName);
            Assert.Equal("Developer Advocate", job.Position);
            Assert.Equal(JobStatus.Interview, job.Status);
            Assert.Equal(new DateTime(2025, 8, 5), job.DateApplied);
        }
    }
}
