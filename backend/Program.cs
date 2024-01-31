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
builder.Services.AddDistributedMemoryCache(); // Required to use session
builder.Services.AddSession();


builder.Services.AddDbContext<MyContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))); // replace with your MySQL version
});

builder.Services.AddCors(options => { options.AddPolicy("AllowSpecificOrigin", builder => { builder.WithOrigins("http://localhost:5173") .AllowAnyMethod() .AllowAnyHeader(); }); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowSpecificOrigin");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

UpdateLateStatuses(app);

app.Run();

void UpdateLateStatuses(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<MyContext>();

        var pendingSessions = context.TrainingSessions
            .Where(ts => !ts.IsCompleted && DateTime.Now > ts.DueDate);

        foreach (var session in pendingSessions)
        {
            session.IsLate = true;
        }

        context.SaveChanges();
    }
}
