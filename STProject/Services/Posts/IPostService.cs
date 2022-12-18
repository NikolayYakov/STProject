using STProject.Data.Models;

namespace STProject.Services.Posts
{
    public interface IPostService
    {
        public int Create(Post post);

        public Post Details(int? id);

        public List<Post> Search(int communityId, string searchTerm = null);
    }
}
