using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    interface ILessonRepository
    {
        Lesson GetSingleLesson(string id);

        List<Lesson> GetAllLesson();

        List<Lesson> GetAllLessonByCourseID(string courseID);

        List<string> GetLessonsName();

        void AddLesson(Lesson lesson);

        void UpdateLesson(Lesson lesson);

        void DeleteLesson(Lesson lesson);
    }
}
