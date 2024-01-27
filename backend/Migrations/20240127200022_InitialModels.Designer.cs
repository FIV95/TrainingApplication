﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Models;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240127200022_InitialModels")]
    partial class InitialModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VideoLink")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("ExerciseSet", b =>
                {
                    b.Property<int>("ExerciseSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("TrainingSessionExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("ExerciseSetId");

                    b.HasIndex("TrainingSessionExerciseId");

                    b.ToTable("ExerciseSets");
                });

            modelBuilder.Entity("TrainingSession", b =>
                {
                    b.Property<int>("TrainingSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("CoachId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TrainingSessionId");

                    b.HasIndex("ClientId");

                    b.HasIndex("CoachId");

                    b.ToTable("TrainingSessions");
                });

            modelBuilder.Entity("TrainingSessionExercise", b =>
                {
                    b.Property<int>("TrainingSessionExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("TrainingSessionId")
                        .HasColumnType("int");

                    b.HasKey("TrainingSessionExerciseId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("TrainingSessionId");

                    b.ToTable("TrainingSessionExercises");
                });

            modelBuilder.Entity("UserBase", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("UserBase");
                });

            modelBuilder.Entity("Client", b =>
                {
                    b.HasBaseType("UserBase");

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.HasIndex("CoachId");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("Coach", b =>
                {
                    b.HasBaseType("UserBase");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("Comment", b =>
                {
                    b.HasOne("UserBase", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExerciseSet", b =>
                {
                    b.HasOne("TrainingSessionExercise", "TrainingSessionExercise")
                        .WithMany()
                        .HasForeignKey("TrainingSessionExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingSessionExercise");
                });

            modelBuilder.Entity("TrainingSession", b =>
                {
                    b.HasOne("Client", "Client")
                        .WithMany("TrainingSessions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("TrainingSessionExercise", b =>
                {
                    b.HasOne("Exercise", "Exercise")
                        .WithMany("TrainingSessionExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainingSession", "TrainingSession")
                        .WithMany("TrainingSessionExercises")
                        .HasForeignKey("TrainingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("TrainingSession");
                });

            modelBuilder.Entity("Client", b =>
                {
                    b.HasOne("Coach", "Coach")
                        .WithMany("Clients")
                        .HasForeignKey("CoachId");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("Exercise", b =>
                {
                    b.Navigation("TrainingSessionExercises");
                });

            modelBuilder.Entity("TrainingSession", b =>
                {
                    b.Navigation("TrainingSessionExercises");
                });

            modelBuilder.Entity("UserBase", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Client", b =>
                {
                    b.Navigation("TrainingSessions");
                });

            modelBuilder.Entity("Coach", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
