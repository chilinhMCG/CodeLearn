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
    public class CourseDetailRepository : ICourseDetailRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public CourseDetailRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddCourseDetail(CourseDetail courseDetail)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.CourseDetails.Add(courseDetail);
            context.SaveChanges();
        }

        public void UpdateCourseDetail(CourseDetail courseDetail)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.CourseDetails.Update(courseDetail);
            context.SaveChanges();
        }

        public void DeleteCourseDetail(CourseDetail courseDetail)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.CourseDetails.Remove(courseDetail);
            context.SaveChanges();
        }

        public List<CourseDetail> GetAllCourseDetail()
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.CourseDetails.OrderBy(x => x.Name).ToList();
        }
    }
}
