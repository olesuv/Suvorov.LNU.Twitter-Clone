using Suvorov.LNU.TwitterClone.Models.Database;

public interface IPostRecommendations
{
    Task<List<Post>> GeneratePostRecommendations(User currentUser);
}
