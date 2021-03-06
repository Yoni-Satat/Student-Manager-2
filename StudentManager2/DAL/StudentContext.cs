﻿using StudentManager2.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StudentManager2.DAL
{
    public class StudentContext : DbContext
    {

        public StudentContext() : base("StudentContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public object selectedStudent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<StudyGroup>()
               .HasMany(g => g.Students).WithMany(s => s.StudyGroups)
               .Map(t => t.MapLeftKey("GroupID")
                   .MapRightKey("StudentID")
                   .ToTable("StudentGroup"));

            modelBuilder.Entity<Student>()
                 .HasMany(s => s.AttendanceRecords).WithMany(a => a.Students)
                 .Map(t => t.MapLeftKey("StudentID")
                 .MapRightKey("AttendanceRecordID")
                 .ToTable("StudentAttendanceRecord"));

            modelBuilder.Entity<Lesson>()
                .HasOptional(l => l.Location)
                .WithMany()
                .HasForeignKey(l => l.LocationID);

            modelBuilder.Entity<AttendanceRecord>()
                .HasOptional(ar => ar.Location)
                .WithMany()
                .HasForeignKey(ar => ar.LocationID);
            
        }        
    }
}