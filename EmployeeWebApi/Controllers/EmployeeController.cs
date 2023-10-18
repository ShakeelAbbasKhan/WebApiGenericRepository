using CourseProject.Business.Services;
using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Dtos.Employee;
using CourseProject.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreate employeeCreate)
        {
            var id = await _employeeService.CreateEmployeeAsync(employeeCreate);
            return Ok(id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdate employeeUpdate)
        {
            await _employeeService.UpdateEmployeeAsync(employeeUpdate);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteEmployee(EmployeeDelete employeeDelete)
        {
            await _employeeService.DeleteEmployeeAsync(employeeDelete);
            return Ok();
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            return Ok(employee);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {
            var employee = await _employeeService.GetEmployeesAsnyc(employeeFilter);
            return Ok(employee);
        }


    }
}
