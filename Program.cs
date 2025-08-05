using JobTrack360.DataEF;
using JobTrack360.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); //dependancy injection for controllers

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("JobTrackDb")); // Use InMemoryDatabase for simplicity
builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>(); // Register the repository

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "JobTrack360 API",
        Version = "v1",
        Description = "API for managing job applications JobTrack360 | Developed by Harshana Perera <harshanatxn@gmail.com>"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
