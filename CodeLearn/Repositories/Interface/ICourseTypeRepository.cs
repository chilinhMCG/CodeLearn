using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface ICourseTypeRepository
    {
        List<CourseType> GetAllCourseType();

        void AddCourseType(CourseType courseType);
    }
}
