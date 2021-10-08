using CodeLearn.Data;
using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public CourseRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void UpdateCourseRating(Course course, int value)
        {
            using var context = _applicationDbContext.CreateDbContext();
            if (value > 0)
            {
                course.TotalRating += value;
                course.RateCount += 1;
                course.DoubleAverageRate = course.TotalRating / course.RateCount;
                course.IntAverageRate = (int)course.DoubleAverageRate;

                context.Courses.Update(course);
                context.SaveChanges();
            }
        }

        public void AddCourse(Course course)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Courses.Update(course);
            context.SaveChanges();
        }

        public void DeleteCourse(Course course)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Courses.Remove(course);
            context.SaveChanges();
        }

        public List<Course> GetAllCourse()
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Courses.OrderBy(x => x.Name).ToList();
        }

    }
}
