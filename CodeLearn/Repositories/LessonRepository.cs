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
    public class LessonRepository : ILessonRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public LessonRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddLesson(Lesson lesson)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Lessons.Add(lesson);
            context.SaveChanges();
        }

        public void UpdateLesson(Lesson lesson)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Lessons.Update(lesson);
            context.SaveChanges();
        }

        public void DeleteLesson(Lesson lesson)
        {
            using var context = _applicationDbContext.CreateDbContext();
            context.Lessons.Remove(lesson);
            context.SaveChanges();
        }

        public List<Lesson> GetAllLesson()
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Lessons.OrderBy(x => x.CreatedAt).ToList();
        }

        public List<Lesson> GetAllLessonByCourseID(string courseID)
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Lessons.Where(x => x.CourseId.ToString() == courseID).OrderBy(x => x.CreatedAt).ToList();
        }

        public Lesson GetSingleLesson(string id)
        {
            using var context = _applicationDbContext.CreateDbContext();
            return context.Lessons.FirstOrDefault(x => x.Id.ToString() == id);
        }
    }
}
