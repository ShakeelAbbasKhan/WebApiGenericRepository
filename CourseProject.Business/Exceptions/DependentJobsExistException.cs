using CourseProject.Common.Model;
using System.Runtime.Serialization;

namespace CourseProject.Business.Exceptions
{
    [Serializable]
    public class DependentJobsExistException : Exception
    {
        public List<Employee> _employees;

        public DependentJobsExistException()
        {
        }

        public DependentJobsExistException(List<Employee> employees)
        {
            _employees = employees;
        }

        public DependentJobsExistException(string? message) : base(message)
        {
        }

        public DependentJobsExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DependentJobsExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}