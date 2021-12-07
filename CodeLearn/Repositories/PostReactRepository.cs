using CodeLearn.Data;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class PostReactRepository : IPostReactRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public PostReactRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public int CountPostReactFromPost(string postid)
        {
            using var context = _applicationDbContext.CreateDbContext();
            var result =  (from p in context.PostReacts
                         where p.PostId.ToString() == postid
                         select p).Count();
            return result; 
        }
    }
}
