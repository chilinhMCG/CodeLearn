using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    interface ICourseDetailRepository
    {
        List<CourseDetail> GetAllCourseDetail();

        void AddCourseDetail(CourseDetail courseDetail);

        void UpdateCourseDetail(CourseDetail courseDetail);

        void DeleteCourseDetail(CourseDetail courseDetail);
    }
}
