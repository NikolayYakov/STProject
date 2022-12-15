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

        public List<Post> Search(string searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return data.Posts.ToList();

            return data.Posts.Where(c => c.Title.Contains(searchTerm) ||
                                              c.Content.Contains(searchTerm)).ToList();
        }
    }
}
