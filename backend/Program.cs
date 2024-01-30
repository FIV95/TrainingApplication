using backend.Data;
using backend.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;

// Rest of your using directives

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = null;
});

DotNetEnv.Env.Load();

builder.Services.AddDistributedMemoryCache(); // Required to use session
builder.Services.AddSession();

builder.Services.AddDbContext<MyContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))); // replace with your MySQL version
});

// Added CORS to communicate with the front end
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Added CORs middleware
app.UseCors("AllowSpecificOrigin");

// Rest of your code
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseCors();
app.MapControllers();

app.Run();
