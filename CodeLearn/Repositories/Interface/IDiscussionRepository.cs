using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IDiscussionRepository
    {
        void AddDiscussion(Discussion discussion);
        Discussion GetDiscussionById(Guid id);
        void UpdateDiscussion(Discussion discussion);
        void DeleteDiscussionByID(Guid id);
        List<Discussion> GetDiscussionPage(int pageNumbers, int pageSize, string search);
        int GetPageNumbers(int sizePage, string search);
        Task<ICollection<Discussion>> GetDiscussionByAuthor(Guid id);
    }
}
