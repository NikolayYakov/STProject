using System;
using STProject.Data.Models;

namespace STProject.Services.Comments
{
	public interface ICommentService
	{
        public int Create(Comment comment);

        public int Edit(Comment comment);

        public int Delete(Comment comment);

        public List<Comment> GetAllByPost(int id);

    }
}

