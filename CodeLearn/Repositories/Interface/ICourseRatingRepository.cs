using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeLearn.Models;

namespace CodeLearn.Repositories.Interface
{
    public interface ICourseRatingRepository
    {
        void CreateNewRating(CourseRating courseRating);

        void UpdateRating(CourseRating courseRating, int value);

        void DeleteRating(CourseRating courseRating);

        List<CourseRating> GetAllCourseRatingByCourseId(string courseId);

        CourseRating GetCourseRatingByUserAndCourseId(string courseId, string userId);

        int GetTotalRatingByCourseId(string courseId);

        int GetTotalRateCountByCourseId(string courseId);
    }
}
