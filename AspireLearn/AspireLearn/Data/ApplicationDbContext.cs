
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany()
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithMany(u => u.EnrolledCourses)
            .UsingEntity<Dictionary<string, object>>(
                "CourseUser",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("StudentsId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<Course>()
                    .WithMany()
                    .HasForeignKey("EnrolledCoursesId")
                    .OnDelete(DeleteBehavior.Restrict)
            );

        builder.Entity<Review>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
