using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;

namespace UniversityApp.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department).WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trainee>()
                .HasOne(t => t.Department).WithMany(d => d.Trainees)
                .HasForeignKey(t => t.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department).WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Course).WithMany(c => c.Instructors)
                .HasForeignKey(i => i.CrsId)
                .OnDelete(DeleteBehavior.SetNull).IsRequired(false);

            modelBuilder.Entity<CrsResult>()
                .HasOne(cr => cr.Course).WithMany(c => c.CrsResults)
                .HasForeignKey(cr => cr.CrsId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CrsResult>()
                .HasOne(cr => cr.Trainee).WithMany(t => t.CrsResults)
                .HasForeignKey(cr => cr.TraineeId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Information Technology", Manager = "Dr. Ahmed Hassan" },
                new Department { Id = 2, Name = "Computer Science", Manager = "Dr. Sara Ali" },
                new Department { Id = 3, Name = "Software Engineering", Manager = "Dr. Omar Khaled" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Windows Programming", Degree = 100, MinDegree = 50, Hrs = 3, DepartmentId = 1 },
                new Course { Id = 2, Name = "Database Systems", Degree = 100, MinDegree = 50, Hrs = 3, DepartmentId = 1 },
                new Course { Id = 3, Name = "Web Development", Degree = 100, MinDegree = 50, Hrs = 3, DepartmentId = 2 },
                new Course { Id = 4, Name = "Data Structures", Degree = 100, MinDegree = 50, Hrs = 3, DepartmentId = 2 },
                new Course { Id = 5, Name = "Software Design", Degree = 100, MinDegree = 50, Hrs = 3, DepartmentId = 3 }
            );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, Name = "Mahmoud Alghonimy", Email = "mahmoud@university.edu", Salary = 8000, Address = "Alexandria, Egypt", DepartmentId = 1, CrsId = 1 },
                new Instructor { Id = 2, Name = "Sara Ali", Email = "sara@university.edu", Salary = 7500, Address = "Cairo, Egypt", DepartmentId = 2, CrsId = 3 },
                new Instructor { Id = 3, Name = "Ahmed Mostafa", Email = "ahmed@university.edu", Salary = 9000, Address = "Giza, Egypt", DepartmentId = 1, CrsId = 2 }
            );

            modelBuilder.Entity<Trainee>().HasData(
                new Trainee { Id = 1, Name = "Ali Hassan", Address = "Alexandria", Grade = "A", DepartmentId = 1 },
                new Trainee { Id = 2, Name = "Nour Mohamed", Address = "Cairo", Grade = "B", DepartmentId = 2 },
                new Trainee { Id = 3, Name = "Layla Ahmed", Address = "Mansoura", Grade = "A", DepartmentId = 1 },
                new Trainee { Id = 4, Name = "Omar Ibrahim", Address = "Tanta", Grade = "C", DepartmentId = 3 }
            );

            modelBuilder.Entity<CrsResult>().HasData(
                new CrsResult { Id = 1, Degree = 85, CrsId = 1, TraineeId = 1 },
                new CrsResult { Id = 2, Degree = 72, CrsId = 2, TraineeId = 1 },
                new CrsResult { Id = 3, Degree = 90, CrsId = 3, TraineeId = 2 },
                new CrsResult { Id = 4, Degree = 65, CrsId = 4, TraineeId = 2 },
                new CrsResult { Id = 5, Degree = 78, CrsId = 1, TraineeId = 3 }
            );
        }
    }
}