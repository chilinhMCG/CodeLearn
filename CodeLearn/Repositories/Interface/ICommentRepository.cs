using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface ICommentRepository
    {
        List<Comment> GetAllCommentInPost(Guid postID);
        void AddComment(Comment comment);
        void DeleteAllCommentsInPost(Guid postID);
        public void DeleteCommentbyObject(Comment comment);
    }
}
