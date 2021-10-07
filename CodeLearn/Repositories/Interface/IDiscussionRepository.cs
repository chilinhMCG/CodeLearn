using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IDiscussionRepository
    {
        List<Discussion> GetAllDiscussionType();
        void AddDiscussion(Discussion discussion);
        Discussion GetDiscussionById(Guid id);
        void UpdateDiscussion(Discussion discussion);
        void DeleteDiscussionByID(Guid id);
    }
}
