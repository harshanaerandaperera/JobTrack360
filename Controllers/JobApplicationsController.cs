using JobTrack360.Models;
using JobTrack360.Repositories;
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
        /// <returns></returns>
        [HttpGet]
        public async Task <IActionResult> GetAll() => Ok(await _repo.GetAllAsync());


        // GET api/<JobApplicationsController>
        /// <summary>
        /// retrieves a job application by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _repo.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound();
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
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(JobApplication application)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            application.DateApplied = DateTime.UtcNow; // Set the date when the application was submitted
            var createdApplication = await _repo.AddAsync(application);
            return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
        }

        // PUT api/<JobApplicationsController>
        /// <summary>
        /// updates an existing job application.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, JobApplication application)
        {
            if (id != application.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedApplication = await _repo.UpdateAsync(application);
            
            if (updatedApplication == null)
            {
                return NotFound();
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _repo.GetByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            else 
            {
                await _repo.DeleteAsync(id);
                return NoContent();
            }
              
        }
    }
}
