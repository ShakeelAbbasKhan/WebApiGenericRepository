using AutoMapper;
using CourseProject.Business.Exceptions;
using CourseProject.Business.Validation;
using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Dtos.Job;
using CourseProject.Common.Interface;
using CourseProject.Common.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Services
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Job> _genericRepository;
        private readonly JobCreateValidator _jobCreateValidator;
        private readonly JobUpdateValidator _jobUpdateValidator;

        public JobService(IMapper mapper, IGenericRepository<Job> genericRepository
            , JobCreateValidator jobCreateValidator, JobUpdateValidator jobUpdateValidator)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _jobCreateValidator = jobCreateValidator;
            _jobUpdateValidator = jobUpdateValidator;
        }
        public async Task<int> CreateJobAysnc(JobCreate jobCreate)
        {
            await _jobCreateValidator.ValidateAndThrowAsync(jobCreate);

            var entity = _mapper.Map<Job>(jobCreate);
            await _genericRepository.InsertAsync(entity);
            await _genericRepository.SaveChangesAsync();
            return entity.Id;

        }

        public async Task DeleteAsync(JobDeleted jobDeleted)
        {
            var entity = await _genericRepository.GetByIdAsync(jobDeleted.Id, (job)=>job.Employees);

            if (entity == null) {
                throw new JobNotFoundException(jobDeleted.Id);
            }
            if(entity.Employees.Count > 0) {
                throw new DependentJobsExistException(entity.Employees);
            }

             _genericRepository.Delete(entity);
            await _genericRepository.SaveChangesAsync();

        }

        public async Task<JobGet> GetJobAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new JobNotFoundException(id);
            }
            return _mapper.Map<JobGet>(entity);
            
        }

        public async Task<List<JobGet>> GetJobsAsync()
        {
            var entity = await _genericRepository.GetAsync(null,null);
            return _mapper.Map<List<JobGet>>(entity);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            await _jobUpdateValidator.ValidateAndThrowAsync(jobUpdate);

            var existingJob = await _genericRepository.GetByIdAsync(jobUpdate.Id);

            if (existingJob == null)
                throw new AddressNotFoudException(existingJob.Id);

            var entity = _mapper.Map<Job>(jobUpdate);
            _genericRepository.UpdateAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }
    }
}
