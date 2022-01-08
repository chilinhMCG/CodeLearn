using Bogus;
using CodeLearn.Models;
using ManageForum.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { 
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { set; get; }
        public DbSet<PostReact> PostReacts { set; get; }

        public DbSet<DiscussionReact> DiscussionReacts { set; get; }
        
        public DbSet<CourseRating> CourseRatings { get; set; }

        static ApplicationDBContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<CourseStatusEnum>();
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
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
            modelBuilder.Entity<Post>()
              .HasOne(h => h.User)
              .WithMany(t => t.Posts)
              .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<Comment>()
               .HasOne(h => h.Discussion)
               .WithMany(t => t.Comments);
            modelBuilder.Entity<Comment>()
               .HasOne(h => h.Post)
               .WithMany(t => t.Comments);
            modelBuilder.Entity<Comment>()
               .HasOne(h => h.User)
              .WithMany(t => t.Comments)
              .HasForeignKey(t => t.UserId);

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
            modelBuilder.Entity<PostReact>()
                .HasKey(nameof(PostReact.PostId), nameof(PostReact.UserId));
            modelBuilder.Entity<DiscussionReact>()
                .HasKey(nameof(DiscussionReact.DiscussionId), nameof(DiscussionReact.UserId));

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
        }
    }
}
