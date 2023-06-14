using Suvorov.LNU.TwitterClone.Models.Database;

public interface IRecommendations
{
    Task<List<Post>> GeneratePostRecommendations(User currentUser);

    Task<List<User>> GeneratePeopleRecommendations(User currentUser);
}
