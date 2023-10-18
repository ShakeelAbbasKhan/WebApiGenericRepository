using CourseProject.Business;
using CourseProject.Common.Interface;
using CourseProject.Common.Model;
using CourseProject.Infrastructure;
using EmployeeWebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Mapper
DIConfiguration.RegisterServices(builder.Services);
builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IGenericRepository<Job>, GenericRepository<Job>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();
// configure connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// exceptionmiddleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
