using CodeLearn.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CourseRating> CourseRatings { get; set; }

        static ApplicationDBContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<CourseStatusEnum>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<CourseStatusEnum>();

            modelBuilder.Entity<Course>(entity =>
                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"));
            modelBuilder.Entity<Course>()
                .HasOne(h => h.CourseTypeNavigation)
                .WithMany(h => h.Courses)
                .HasForeignKey(h => h.CourseTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_courses_coursestype");

            modelBuilder.Entity<Lesson>(entity =>
                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"));
            modelBuilder.Entity<Lesson>()
                .HasOne(h => h.CourseNavigation)
                .WithMany(h => h.Lessons)
                .HasForeignKey(h => h.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Lesson_course");

            modelBuilder.Entity<Lesson>()
                .HasOne(h => h.UserNavigation)
                .WithMany(h => h.Lessons)
                .HasForeignKey(h => h.CreatorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_lesson_user");

            modelBuilder.Entity<CourseRating>()
                .HasKey(nameof(CourseRating.CourseId), nameof(CourseRating.UserId));
        }
    }
}
