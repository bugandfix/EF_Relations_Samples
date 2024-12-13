namespace EF_Relations.Data;
using EF_Relations.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : DbContext(options)
{
        //==> Migration Commands

        // dotnet ef migrations add InitialCreate
        // dotnet ef database update
        public DbSet<TaxPayer>? Taxpayers { get; set; }
        public DbSet<TaxRecord>? TaxRecords { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Flunet API
        modelBuilder.Entity<TaxPayer>()
       .HasOne(u => u.Taxrecord)
       .WithOne(p => p.Taxpayer)
       .HasForeignKey<TaxRecord>(p => p.TaxPayerID)
       .OnDelete(DeleteBehavior.Cascade); // ===> Optional: Cascade delete behavior


        //Flunet API
        modelBuilder.Entity<Post>()
        .HasOne(p => p.Blog)
        .WithMany(b => b.Posts)
        .HasForeignKey(p => p.BlogId);



        modelBuilder.Entity<StudentCourse>()
         .HasOne(sc => sc.Student)
         .WithMany(s => s.StudentCourses)
         .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);

        // Configure Many-to-Many relationship between Students and Courses
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });



        #region Seed-Data


        // Seed data for Students
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Ali" },
            new Student { Id = 2, Name = "Reza" },
            new Student { Id = 3, Name = "Farhad" }
        );

        // Seed data for Courses
        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "C#" },
            new Course { Id = 2, Name = "SQL Server" },
            new Course { Id = 3, Name = "Printing" }
        );

        // Seed data for StudentCourse
        modelBuilder.Entity<StudentCourse>().HasData(
            new StudentCourse { StudentId = 1, CourseId = 1 },
            new StudentCourse { StudentId = 1, CourseId = 2 },
            new StudentCourse { StudentId = 2, CourseId = 2 },
            new StudentCourse { StudentId = 3, CourseId = 3 }
        );


        // Seed Data for TaxPayers
        modelBuilder.Entity<TaxPayer>().HasData(
            new TaxPayer { Id = 1, FullName = "Ali" },
            new TaxPayer { Id = 2, FullName = "Reza" },
            new TaxPayer { Id = 3, FullName = "Sara" }
        );

        // Seed Data for TaxRecords
        modelBuilder.Entity<TaxRecord>().HasData(
            new TaxRecord { Id = 1, TaxCode = "TAX12345", TotalTaxPaid = 5000.00M, TaxPayerID = 1 },
            new TaxRecord { Id = 2, TaxCode = "TAX67890", TotalTaxPaid = 7500.00M, TaxPayerID = 2 },
            new TaxRecord { Id = 3, TaxCode = "TAX54321", TotalTaxPaid = 3000.00M, TaxPayerID = 3 }
        );




        // Seed Data for Blogs
        modelBuilder.Entity<Blog>().HasData(
        new Blog { Id = 1, Title = "BugandFix" },
        new Blog { Id = 2, Title = "Travel Diaries" },
        new Blog { Id = 3, Title = "Food Adventures" });

        // Seed Data for Posts
        modelBuilder.Entity<Post>().HasData(
        new Post { Id = 1, Content = "C# 13", BlogId = 1 },
        new Post { Id = 2, Content = "SQL Server", BlogId = 1 },
        new Post { Id = 3, Content = "Exploring Japan", BlogId = 2 },
        new Post { Id = 4, Content = "Backpacking Europe", BlogId = 2 },
        new Post { Id = 5, Content = "Best Street Foods", BlogId = 3 },
        new Post { Id = 6, Content = "Gourmet Experiences", BlogId = 3 });

        #endregion

    }
}
