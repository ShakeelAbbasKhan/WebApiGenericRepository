﻿using System.Runtime.Serialization;

namespace CourseProject.Business.Exceptions
{
    [Serializable]
    public class EmployeesNotFoundException : Exception
    {
        public int[] EmployeeIds { get; }

        public EmployeesNotFoundException()
        {
        }

        public EmployeesNotFoundException(int[] ints)
        {
            EmployeeIds = ints;
        }

        public EmployeesNotFoundException(string? message) : base(message)
        {
        }

        public EmployeesNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmployeesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}