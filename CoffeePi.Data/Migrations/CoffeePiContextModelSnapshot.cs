﻿// <auto-generated />
using System;
using CoffeePi.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeePi.Data.Migrations
{
    [DbContext(typeof(CoffeePiContext))]
    partial class CoffeePiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CoffeePi.Data.Models.CoffeeRoutine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ButtonType")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("CoffeeRoutines");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CoffeeRoutine");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.ExecutedRoutine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DailyRoutineId")
                        .HasColumnType("int");

                    b.Property<int>("RoutineId")
                        .HasColumnType("int");

                    b.Property<bool>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("WeeklyRoutineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DailyRoutineId");

                    b.HasIndex("RoutineId");

                    b.HasIndex("WeeklyRoutineId");

                    b.ToTable("ExecutedRoutines");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.DailyRoutine", b =>
                {
                    b.HasBaseType("CoffeePi.Data.Models.CoffeeRoutine");

                    b.Property<string>("DaysOfWeek")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeOnly>("TimeOfDay")
                        .HasColumnType("time(6)");

                    b.HasDiscriminator().HasValue("DailyRoutine");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.SingleRoutine", b =>
                {
                    b.HasBaseType("CoffeePi.Data.Models.CoffeeRoutine");

                    b.Property<int?>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasIndex("ExecutionId");

                    b.HasDiscriminator().HasValue("SingleRoutine");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.WeeklyRoutine", b =>
                {
                    b.HasBaseType("CoffeePi.Data.Models.CoffeeRoutine");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("TimeOfDay")
                        .HasColumnType("time(6)")
                        .HasColumnName("WeeklyRoutine_TimeOfDay");

                    b.HasDiscriminator().HasValue("WeeklyRoutine");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.ExecutedRoutine", b =>
                {
                    b.HasOne("CoffeePi.Data.Models.DailyRoutine", null)
                        .WithMany("Executions")
                        .HasForeignKey("DailyRoutineId");

                    b.HasOne("CoffeePi.Data.Models.CoffeeRoutine", "Routine")
                        .WithMany()
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoffeePi.Data.Models.WeeklyRoutine", null)
                        .WithMany("Executions")
                        .HasForeignKey("WeeklyRoutineId");

                    b.Navigation("Routine");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.SingleRoutine", b =>
                {
                    b.HasOne("CoffeePi.Data.Models.ExecutedRoutine", "Execution")
                        .WithMany()
                        .HasForeignKey("ExecutionId");

                    b.Navigation("Execution");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.DailyRoutine", b =>
                {
                    b.Navigation("Executions");
                });

            modelBuilder.Entity("CoffeePi.Data.Models.WeeklyRoutine", b =>
                {
                    b.Navigation("Executions");
                });
#pragma warning restore 612, 618
        }
    }
}
