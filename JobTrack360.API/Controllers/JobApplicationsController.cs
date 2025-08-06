
using JobTrack360.Application.Interfaces;
using JobTrack360.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobTrack360.Controllers
{
    /// <summary>
    /// API controller for managing job applications.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationRepository _repo;
        public JobApplicationsController(IJobApplicationRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<JobApplicationsController>
        /// <summary>
        ///  retrieves all job applications.
        /// </summary>
        /// <returns> A list of job applications. </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<JobApplication>), StatusCodes.Status200OK)]
        public async Task <IActionResult> GetAll() => Ok(await _repo.GetAllAsync());


        // GET api/<JobApplicationsController>
        /// <summary>
        /// retrieves a job application by its ID.
        /// </summary>
        /// <param name="id">The Unique ID of the job application.</param>
        /// <returns>The job application if found; otherwise, 404.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(JobApplication), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _repo.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound("Application not found.");
            }
            else 
            {
                return Ok(application);
            }

        }

        // POST api/<JobApplicationsController>
        /// <summary>
        /// creates a new job application.
        /// </summary>
        /// <param name="application">The job application to add</param>
        /// <returns>The created job application with location header</returns>
        [HttpPost]
        [ProducesResponseType(typeof(JobApplication), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(JobApplication application)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            // If client didn't send the Job Application date, Set the Current Date
            if (application.DateApplied == default)  
            {
                application.DateApplied = DateTime.UtcNow;
            }
            var createdApplication = await _repo.AddAsync(application);
            return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
        }

        // PUT api/<JobApplicationsController>
        /// <summary>
        /// updates an existing job application.
        /// </summary>
        /// <param name="id">ID of the job application to update</param>
        /// <param name="application">Updated job application</param>
        /// <returns>The updated application</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(JobApplication), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, JobApplication application)
        {
            if (id != application.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedApplication = await _repo.UpdateAsync(application);
            
            if (updatedApplication == null)
            {
                return NotFound("Application not found to update.");
            }
            else
            {
                return Ok(updatedApplication);
            }
                
        }

        // DELETE api/<JobApplicationsController>
        /// <summary>
        /// deletes a job application by its ID.
        /// </summary>
        /// <param name="id">ID of the job application to delete</param>
        /// <returns>NoContent if successful; NotFound otherwise</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _repo.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound("Application not found to delete.");
            }
            else 
            {
                await _repo.DeleteAsync(id);
                return NoContent();
            }
              
        }
    }
}