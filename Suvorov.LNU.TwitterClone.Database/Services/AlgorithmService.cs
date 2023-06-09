using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Database
{
    public class PostRecommendations : IPostRecommendations
    {
        private readonly PostService _postService;

        private readonly FolloweeService _followeeService;

        public PostRecommendations(PostService postService, FolloweeService followeeService)
        {
            _postService = postService;
            _followeeService = followeeService;
        }

        public async Task<List<Post>> GeneratePostRecommendations(User currentUser)
        {
            List<User> allFollowingUsers = await _followeeService.GetAllFollowees(currentUser);
            List<Post> allLatest72hMostLiked = await _postService.GetMostLikedPostsForLast72h();
            List<Post> allLatest72hFolloweesPosts = new List<Post>();
            List<Post> userRecommendations = new List<Post>();

            // If new user
            if (await _followeeService.GetAllFollowees(currentUser) == null)
            {
                foreach (var mostLiked in allLatest72hMostLiked)
                {
                    userRecommendations.Add(mostLiked);
                }
            }
            else
            {
                foreach (var followee in allFollowingUsers)
                {
                    var followeePosts = await _postService.SortForLast72h(followee);
                    allLatest72hFolloweesPosts.AddRange(followeePosts);
                }

                for (int i = 0; i < allLatest72hFolloweesPosts.Count; i++)
                {
                    userRecommendations.Add(allLatest72hFolloweesPosts[i]);
                    // After every 3rd followee's post adds 1 popular post
                    if (i % 3 == 0)
                    {
                        Post mostLikedPost = allLatest72hMostLiked.FirstOrDefault();

                        if (mostLikedPost != null)
                        {
                            allLatest72hMostLiked.Remove(mostLikedPost);
                            userRecommendations.Add(mostLikedPost);
                        }
                    }
                    // If no more followees posts left but there are most liked
                    if (i == allLatest72hFolloweesPosts.Count - 1 && allLatest72hMostLiked.Count > 0)
                    {
                        foreach (var mostLiked in allLatest72hMostLiked)
                        {
                            userRecommendations.Add(mostLiked);
                        }
                    }
                }
            }

            return userRecommendations;
        }
    }
}