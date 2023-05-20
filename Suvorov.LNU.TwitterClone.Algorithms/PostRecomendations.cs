using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Algorithms
{
    public class PostRecomendations
    {
        private readonly PostService _postService;

        public PostRecomendations(PostService postService)
        {
            _postService = postService;
        }

        public async Task<List<Post>> GeneratePostRecomendations()
        {
            // Should be modifiyed when will add `Likes` to `Post`.
            var sortedPosts = await _postService.SortByPostDate();
            return sortedPosts;
        }
    }
}