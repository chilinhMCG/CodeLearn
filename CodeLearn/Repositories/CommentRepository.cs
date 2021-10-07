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
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDBContext;
        public CommentRepository(IDbContextFactory<ApplicationDBContext> applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public void AddComment(Comment comment)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Add(comment);
            context.SaveChanges();
        }

        public List<Comment> GetAllCommentInPost(Guid postID)
        {
            using var context = _applicationDBContext.CreateDbContext();
            IList<Comment> _comments = new List<Comment>();
            IList<Comment> k = context.Comments.ToList();
            foreach (var comment in k)
            {
                if (comment.DiscussionId == postID) _comments.Add(comment);
            }
            return (List<Comment>)_comments;
        }
        public void DeleteAllCommentsInPost(Guid postID)
        {
            using var context = _applicationDBContext.CreateDbContext();
            IList<Comment> _comments = context.Comments.ToList();
            foreach(var comment in _comments)
            {
                if (comment.DiscussionId == postID) context.Comments.Remove(comment);
            }
            context.SaveChanges();

        }
        public void DeleteCommentbyObject(Comment comment)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Comments.Remove(comment);
            context.SaveChanges();
        }
    }
}
