namespace Suvorov.LNU.TwitterClone.Algorithms
{
    public class DetectTagsInPost
    {
        public static async Task<List<string>> ExtractTagsFromPost(string userPost)
        {
            List<string> words = await Task.Run(() => userPost.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());

            List<string> tags = new List<string>();

            foreach (var word in words)
            {
                if (word.StartsWith("#"))
                {
                    string cleanedHashtag = word.TrimStart('#');
                    tags.Add(cleanedHashtag);
                }
            }

            return tags;
        }

        public static async Task<string> RemoveTagsFromPost(string userPost)
        {
            List<string> words = await Task.Run(() => userPost.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());

            List<string> cleanedWords = new List<string>();

            foreach (var word in words)
            {
                if (!word.StartsWith("#"))
                {
                    cleanedWords.Add(word);
                }
            }

            string cleanedPost = string.Join(" ", cleanedWords);

            return cleanedPost;
        }
    }
}
