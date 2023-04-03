using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.DAL.Entities;

namespace TMS.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CareerPath> CareerPaths { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>(i =>
            {
                i.Property(i => i.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                i.Property(i => i.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                i.Property(i => i.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                i.HasIndex(i => i.Email, $"IX_{nameof(Instructor)}_{nameof(Instructor.Email)}")
                    .IsUnique();

                i.HasIndex(i => i.PhoneNumber, $"IX_{nameof(Instructor)}_{nameof(Instructor.PhoneNumber)}")
                    .IsUnique();
            });


            modelBuilder.Entity<Trainee>(t =>
            {
                t.Property(t => t.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                t.Property(t => t.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                t.Property(t => t.Address)
                    .HasMaxLength(80);

                t.Property(t => t.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                t.HasIndex(t => t.Email, $"IX_{nameof(Trainee)}_{nameof(Trainee.Email)}")
                    .IsUnique();

                t.HasIndex(t => t.PhoneNumber, $"IX_{nameof(Instructor)}_{nameof(Instructor.PhoneNumber)}")
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

