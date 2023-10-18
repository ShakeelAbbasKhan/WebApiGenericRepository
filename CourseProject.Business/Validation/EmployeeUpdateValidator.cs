using CourseProject.Common.Dtos.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Validation
{
    public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
    {
        public EmployeeUpdateValidator()
        {
            RuleFor(employeeUpdate => employeeUpdate.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(employeeUpdate => employeeUpdate.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
