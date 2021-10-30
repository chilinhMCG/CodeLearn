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
    public class CourseRatingRepository : ICourseRatingRepository
    {
        private IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public CourseRatingRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public void CreateNewRating(CourseRating courseRating)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.CourseRatings.Add(courseRating);
            context.SaveChanges();
        }

        public void UpdateRating(CourseRating courseRating, int value)
        {
            using var context = _applicationDbContext.CreateDbContext();
            if (value > 0)
            {
                courseRating.TotalRating = value;
                
            }
            context.CourseRatings.Update(courseRating);
            context.SaveChanges();
        }

        public void DeleteRating(CourseRating courseRating)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.CourseRatings.Remove(courseRating);
            context.SaveChanges();
        }

        public List<CourseRating> GetAllCourseRatingByCourseId(string courseId)
        {
            using var context = _applicationDbContext.CreateDbContext();
            List<CourseRating> courseRatings = new List<CourseRating>();
            if (context.CourseRatings.Count() > 0)
            {
                courseRatings = context.CourseRatings.Where(x => x.CourseId.ToString() == courseId).ToList();
            }
            return courseRatings;
        }

        public CourseRating GetCourseRatingByUserAndCourseId(string courseId, string userId)
        {
            using var context = _applicationDbContext.CreateDbContext();
            CourseRating current = context.CourseRatings
                .Where(x => x.CourseId.ToString() == courseId && x.UserId.ToString() == userId)
                .FirstOrDefault();
            return current;
        }

        public int GetTotalRatingByCourseId(string courseId)
        {
            using var context = _applicationDbContext.CreateDbContext();
            List<CourseRating> currentCourseRatings = context.CourseRatings.Where(x => x.CourseId.ToString() == courseId).ToList();
            int total = 0;
            if (currentCourseRatings.Count() > 0)
            {
                foreach (var item in currentCourseRatings)
                {
                    total += item.TotalRating;
                }
            }
            return total;
        }

        public int GetTotalRateCountByCourseId(string courseId)
        {
            using var context = _applicationDbContext.CreateDbContext();
            List<CourseRating> currentCourseRatings = context.CourseRatings.Where(x => x.CourseId.ToString() == courseId).ToList();
            int total = 0;
            if (currentCourseRatings.Count() > 0)
            {
                foreach (var item in currentCourseRatings)
                {
                    total += item.RateCount;
                }
            }
            return total;
        }
    }
}
