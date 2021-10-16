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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussions { get; set; } 
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<User> Users { get; set; }

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
            modelBuilder.Entity<Discussion>()
               .HasOne(h => h.User)
               .WithMany(t => t.Discussions)
               .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<Comment>()
               .HasOne(h => h.Discussion)
               .WithMany(t => t.Comments);
            modelBuilder.Entity<Comment>()
               .HasOne(h => h.User)
              .WithMany(t => t.Comments)
              .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<CourseDetail>(entity =>
                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"));
            modelBuilder.Entity<CourseDetail>()
                .HasOne(h => h.CourseNavigation)
                .WithMany(h => h.CourseDetails)
                .HasForeignKey(h => h.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_coursedetail_course");

            modelBuilder.Entity<CourseDetail>()
                .HasOne(h => h.UserNavigation)
                .WithMany(h => h.CourseDetails)
                .HasForeignKey(h => h.CreatorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_coursedetial_user");
        }
    }
}
