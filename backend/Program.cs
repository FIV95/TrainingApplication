using backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add session services
builder.Services.AddDistributedMemoryCache(); // Required to use session
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use session
app.UseSession();

app.Run();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyContext>();
    context.Database.Migrate();
}
