using Courseproject.Business.Services;
using CourseProject.Business.Services;
using CourseProject.Business.Validation;
using CourseProject.Common.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business
{
    public class DIConfiguration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DIConfiguration));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITeamService, TeamService>();

            // register validation

            services.AddScoped<AddressCreateValidator>();
            services.AddScoped<AddressUpdateValidator>();
            services.AddScoped<EmployeeCreateValidator>();
            services.AddScoped<EmployeeUpdateValidator>();
            services.AddScoped<JobCreateValidator>();
            services.AddScoped<JobUpdateValidator>();
            services.AddScoped<TeamCreateValidator>();
            services.AddScoped<TeamUpdateValidator>();
        }
    }
}
