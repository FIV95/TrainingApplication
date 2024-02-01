﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240129005006_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("backend.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TrainingSessionExerciseId")
                        .HasColumnType("int");

                    b.Property<int?>("TrainingSessionId")
                        .HasColumnType("int");

                    b.Property<int>("UserBaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainingSessionExerciseId");

                    b.HasIndex("TrainingSessionId");

                    b.HasIndex("UserBaseId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("backend.Models.Exercise", b =>
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

            modelBuilder.Entity("backend.Models.ExerciseSet", b =>
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

            modelBuilder.Entity("backend.Models.TrainingSession", b =>
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

            modelBuilder.Entity("backend.Models.TrainingSessionExercise", b =>
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

            modelBuilder.Entity("backend.Models.UserBase", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.ToTable("UserBases");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserBase");
                });

            modelBuilder.Entity("backend.Models.Client", b =>
                {
                    b.HasBaseType("backend.Models.UserBase");

                    b.Property<int?>("CoachId")
                        .HasColumnType("int");

                    b.Property<int>("UserBaseId")
                        .HasColumnType("int")
                        .HasColumnName("Client_UserBaseId");

                    b.HasIndex("CoachId");

                    b.HasIndex("UserBaseId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("backend.Models.Coach", b =>
                {
                    b.HasBaseType("backend.Models.UserBase");

                    b.Property<int>("UserBaseId")
                        .HasColumnType("int");

                    b.HasIndex("UserBaseId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("backend.Models.Comment", b =>
                {
                    b.HasOne("backend.Models.TrainingSessionExercise", "TrainingSessionExercise")
                        .WithMany()
                        .HasForeignKey("TrainingSessionExerciseId");

                    b.HasOne("backend.Models.TrainingSession", "TrainingSession")
                        .WithMany("Comments")
                        .HasForeignKey("TrainingSessionId");

                    b.HasOne("backend.Models.UserBase", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingSession");

                    b.Navigation("TrainingSessionExercise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.ExerciseSet", b =>
                {
                    b.HasOne("backend.Models.TrainingSessionExercise", "TrainingSessionExercise")
                        .WithMany()
                        .HasForeignKey("TrainingSessionExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingSessionExercise");
                });

            modelBuilder.Entity("backend.Models.TrainingSession", b =>
                {
                    b.HasOne("backend.Models.Client", "Client")
                        .WithMany("TrainingSessions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("backend.Models.TrainingSessionExercise", b =>
                {
                    b.HasOne("backend.Models.Exercise", "Exercise")
                        .WithMany("TrainingSessionExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.TrainingSession", "TrainingSession")
                        .WithMany("TrainingSessionExercises")
                        .HasForeignKey("TrainingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("TrainingSession");
                });

            modelBuilder.Entity("backend.Models.Client", b =>
                {
                    b.HasOne("backend.Models.Coach", "Coach")
                        .WithMany("Clients")
                        .HasForeignKey("CoachId");

                    b.HasOne("backend.Models.UserBase", null)
                        .WithOne()
                        .HasForeignKey("backend.Models.Client", "UserBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("backend.Models.Coach", b =>
                {
                    b.HasOne("backend.Models.UserBase", null)
                        .WithOne()
                        .HasForeignKey("backend.Models.Coach", "UserBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Models.Exercise", b =>
                {
                    b.Navigation("TrainingSessionExercises");
                });

            modelBuilder.Entity("backend.Models.TrainingSession", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("TrainingSessionExercises");
                });

            modelBuilder.Entity("backend.Models.UserBase", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("backend.Models.Client", b =>
                {
                    b.Navigation("TrainingSessions");
                });

            modelBuilder.Entity("backend.Models.Coach", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}