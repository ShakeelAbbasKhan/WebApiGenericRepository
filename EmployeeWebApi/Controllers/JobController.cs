using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Dtos.Job;
using CourseProject.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateJob(JobCreate jobCreate)
        {
            var id = await _jobService.CreateJobAysnc(jobCreate);
            return Ok(id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAddress(JobUpdate jobUpdate)
        {
            await _jobService.UpdateJobAsync(jobUpdate);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAddress(JobDeleted jobDeleted)
        {
            await _jobService.DeleteAsync(jobDeleted);
            return Ok();
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            var address = await _jobService.GetJobAsync(id);
            return Ok(address);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _jobService.GetJobsAsync();
            return Ok(addresses);
        }
    }
}
