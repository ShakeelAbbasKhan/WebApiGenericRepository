﻿using CourseProject.Business.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EmployeeWebApi
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (EmployeeNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Employee for id {ex.Id} not found.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }

            catch (EmployeesNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Employees {JsonSerializer.Serialize(ex.EmployeeIds)} not found.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }
            catch (TeamNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Team for id {ex.Id} not found.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }


            catch (JobNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Job for id {ex.Id} not found.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }

            catch (DependentJobsExistException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Dependent Employees {JsonSerializer.Serialize(ex._employees.Select(
                        e => e.Id))} exist.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }

            catch (AddressNotFoudException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Address for id {ex.Id} not found.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }


            catch (DependentEmployeesExistException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Dependent Employees {JsonSerializer.Serialize(ex._employees.Select(
                        e => e.Id))} exist.",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }

            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonSerializer.Serialize(ex.Errors),
                    Instance = "",
                    Title = "Validation Error",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);

            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = "",
                    Title = "Internal Server Error - somthing went wrong.",
                    Type = "Error"
                };
                
                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson); 
            }
        }
    }
}
