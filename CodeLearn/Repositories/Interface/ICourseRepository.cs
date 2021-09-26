using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface ICourseRepository
    {

        List<Course> GetAllCourse();

        void AddCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(Course course);

        //int GetCourseRating(Course course);

        void UpdateCourseRating(Course course, int value);

    }
}
