#pragma warning disable CS8618
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data
{
    // the MyContext class represents a session with our MySQL database, allowing us to query for or save data
    // DbContext is a class that comes from EntityFramework, we want to inherit its features
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseSet> ExerciseSets { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<TrainingSessionExercise> TrainingSessionExercises { get; set; }
        public DbSet<UserBase> UserBases { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Coach>().ToTable("Coaches");
            modelBuilder.Entity<UserBase>() // Add this line
                .HasIndex(u => u.Email)
                .IsUnique(); // Add this line
        }
    }
}
