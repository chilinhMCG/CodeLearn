using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface ICourseRepository
    {
        Course GetSingleCourse(string id);

        string GetSingleCourseNameById(string id);

        List<string> GetCoursesName();

        List<Guid> GetCourseTypeId();

        List<Course> GetAllCourse();

        List<Course> GetAllCourseWithSearchString(string searchString);

        void AddCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(Course course);

        void UpdateRating(Course course, int value);

    }
}
