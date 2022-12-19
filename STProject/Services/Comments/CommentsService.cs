using System;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using STProject.Data;
using STProject.Data.Models;

namespace STProject.Services.Comments
{
	public class CommentsService : ICommentService
    {

        private readonly STProjectContext data;

        public CommentsService(STProjectContext data) 
		{
            this.data = data;
        }

        public int Create(Comment comment)
        {
            this.data.Comments.Add(comment);
            this.data.SaveChanges();

            return comment.Id;
        }

        public int Delete(Comment comment)
        {
            this.data.Comments.Remove(comment);

            return comment.Id;
        }

        public int Edit(Comment comment)
        {
            var curr = this.data.Comments.Find(comment.Id);

            if (curr == null)
            {
                return 0;
            }

            curr.Content = comment.Content;

            this.data.SaveChanges();

            return comment.Id;
        }

        public Comment commentDetails(int? id)
        {
            if (id == null || data.Comments == null)
            {
                return null;
            }

            var comment = data.Comments.FirstOrDefault(m => m.Id == id);
            if (comment == null)
            {
                return null;
            }
            else
            {
                return comment;
            }
        }

        public List<Comment> GetAllByPost(int? id)
        {
            var query = data.Comments.AsQueryable();
            query = query.Where(x => x.PostId == id && x.IsDeleted == false);
            return query.ToList();
        }

    }
}

