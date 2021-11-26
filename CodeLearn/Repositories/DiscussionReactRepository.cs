using CodeLearn.Data;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class DiscussionReactRepository : IDiscussionReactRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDbContext;

        public DiscussionReactRepository(IDbContextFactory<ApplicationDBContext> applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public int CountDiscussionReactFromDiscussion(string discussionid)
        {
            using var context = _applicationDbContext.CreateDbContext();
            var result = (from p in context.DiscussionReacts
                          where p.DiscussionId.ToString() == discussionid
                          select p).Count();
            return result;
        }
    }
}
