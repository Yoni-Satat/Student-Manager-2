using StudentManager2.Models;
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
        public object selectedStudent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<StudyGroup>()
               .HasMany(g => g.Students).WithMany(s => s.StudyGroups)
               .Map(t => t.MapLeftKey("GroupID")
                   .MapRightKey("StudentID")
                   .ToTable("StudentGroup"));

            
        }        
    }
}