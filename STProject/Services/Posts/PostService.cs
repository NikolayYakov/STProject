﻿using Microsoft.EntityFrameworkCore;
using STProject.Data;
using STProject.Data.Models;

namespace STProject.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly STProjectContext data;

        public PostService(STProjectContext data)
        {
            this.data = data;
        }

        public async Task<List<Post>> GetPaginatedResult(List<Post> posts, int currentPage, int pageSize = 1)
        {
            return posts.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public int Create(Post post)
        {
            this.data.Posts.Add(post);
            this.data.SaveChanges();

            return post.Id;
        }

        public Post Details(int? id)
        {
            if (id == null || data.Posts == null)
            {
                return null;
            }

            var post = data.Posts.FirstOrDefault(m => m.Id == id);
            if (post == null)
            {
                return null;
            }
            else
            {
                return post;
            }
        }

        public List<Post> Search(int communityId, string searchTerm = null)
        {
            var query = data.Posts.AsQueryable();
            query = query.Where(x => x.CommunityId == communityId);
            if (string.IsNullOrEmpty(searchTerm))
                return query.ToList();

            return query.Where(c => c.Title.Contains(searchTerm) ||
                                              c.Content.Contains(searchTerm)).ToList();
        }
    }
}
