using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Common.Dtos.Employee;

namespace CourseProject.Common.Interface
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
        Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
        Task<List<EmployeeList>> GetEmployeesAsnyc(EmployeeFilter employeeFilter);
        Task<EmployeeDetails> GetEmployeeAsync(int id);
        Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);
    }
}
