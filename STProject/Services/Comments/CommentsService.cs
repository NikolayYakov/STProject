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

        public List<Comment> GetAllByPost(int id)
        {
            return data.Comments.Where(c => c.PostId == id ).ToList();
        }
    }
}

