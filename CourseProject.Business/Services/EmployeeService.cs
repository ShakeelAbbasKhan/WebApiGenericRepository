using AutoMapper;
using CourseProject.Business.Exceptions;
using CourseProject.Business.Validation;
using CourseProject.Common.Dtos.Employee;
using CourseProject.Common.Interface;
using CourseProject.Common.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Job> _jobRepository;
        private readonly IGenericRepository<Address> _addressRepository;
        private readonly EmployeeCreateValidator _employeeCreateValidator;
        private readonly EmployeeUpdateValidator _employeeUpdateValidator;

        public EmployeeService(IMapper mapper, IGenericRepository<Employee> employeeRepository
            , IGenericRepository<Job> jobRepository, IGenericRepository<Address> addressRepository,
            EmployeeCreateValidator employeeCreateValidator, EmployeeUpdateValidator employeeUpdateValidator)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _jobRepository = jobRepository;
            _addressRepository = addressRepository;
            _employeeCreateValidator = employeeCreateValidator;
            _employeeUpdateValidator = employeeUpdateValidator;
        }
        public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
        {
            await _employeeCreateValidator.ValidateAndThrowAsync(employeeCreate);

            var address = await _addressRepository.GetByIdAsync(employeeCreate.AddressId);

            if(address == null)
            {
                throw new AddressNotFoudException(employeeCreate.AddressId);
            }

            var job = await _jobRepository.GetByIdAsync(employeeCreate.JobId);

            if(job == null) {
                throw new JobNotFoundException(employeeCreate.JobId);
            }

            var entity  = _mapper.Map<Employee>(employeeCreate);
            entity.Address = address;
            entity.Job = job;
            await _employeeRepository.InsertAsync(entity);
            await _employeeRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
        {
            var entity = await _employeeRepository.GetByIdAsync(employeeDelete.id);

            if (entity == null)
            {
                throw new EmployeeNotFoundException(employeeDelete.id);
            }

            _employeeRepository.Delete(entity);
            await _employeeRepository.SaveChangesAsync();

        }

        public async Task<EmployeeDetails> GetEmployeeAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id, (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);

            if(entity == null)
            {
                throw new EmployeeNotFoundException(id);
            }
            return _mapper.Map<EmployeeDetails>(entity);
        }

        public async Task<List<EmployeeList>> GetEmployeesAsnyc(EmployeeFilter employeeFilter)
        {
            Expression<Func<Employee, bool>> firstNameFilter = (employee) => employeeFilter.FirstName == null ? true :
            employee.FirstName.StartsWith(employeeFilter.FirstName);
            Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeeFilter.LastName == null ? true :
            employee.LastName.StartsWith(employeeFilter.LastName);
            Expression<Func<Employee, bool>> jobfilter = (employee) => employeeFilter.Job == null ? true :
            employee.Job.Name.StartsWith(employeeFilter.Job);
            Expression<Func<Employee, bool>> teamFilter = (employee) => employeeFilter.Team == null ? true :
            employee.Teams.Any(team => team.Name.StartsWith(employeeFilter.Team));

            var entities = await _employeeRepository.GetFilterAsync(new Expression<Func<Employee, bool>>[]
            {
            firstNameFilter, lastNameFilter, jobfilter, teamFilter
            }, employeeFilter.Skip, employeeFilter.Take,
            (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);

            return _mapper.Map<List<EmployeeList>>(entities);
        }

        public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
        {
            await _employeeUpdateValidator.ValidateAndThrowAsync(employeeUpdate);

            var address = await _addressRepository.GetByIdAsync(employeeUpdate.AddressId);
            if (address == null)
            {
                throw new AddressNotFoudException(employeeUpdate.AddressId);
            }
            var job = await _jobRepository.GetByIdAsync(employeeUpdate.JobId);
            if (job == null)
            {
                throw new AddressNotFoudException(employeeUpdate.JobId);
            }

            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeUpdate.Id);
            if (existingEmployee == null)
            {
                throw new JobNotFoundException(employeeUpdate.Id);
            }

            var entity = _mapper.Map<Employee>(employeeUpdate);
            entity.Address = address;
            entity.Job = job;
            _employeeRepository.UpdateAsync(entity);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}
