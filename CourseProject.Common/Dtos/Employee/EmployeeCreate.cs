﻿using System.ComponentModel.DataAnnotations;

namespace CourseProject.Common.Dtos.Employee
{
    public record EmployeeCreate(string FirstName, string LastName, int AddressId, int JobId);
}