using CourseProject.Common.Dtos.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Interface
{
    public interface IJobService
    {
        Task<int> CreateJobAysnc(JobCreate jobCreate);
        Task UpdateJobAsync(JobUpdate jobUpdate);
        Task DeleteAsync(JobDeleted jobDeleted);
        Task<JobGet> GetJobAsync(int id);
        Task<List<JobGet>> GetJobsAsync();
    }
}
