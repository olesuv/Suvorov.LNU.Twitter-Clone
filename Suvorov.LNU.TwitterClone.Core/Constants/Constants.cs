namespace Suvorov.LNU.TwitterClone.Core.Constants
{
    public static class ProjectConstants
    {
        public static readonly string[] OpenAIPromptTweetText = new[]
        {
            @"You are going to be Twitter writer.
            Here is my idea, about which I would like to write.
            Your main goal is to write me a tweet which is going to be viral. 
            Style of text should be polite. Max tweet characters is 100. 
            Do not write any comments to tweet, only tweet text. 
            Don't include any hashtags. Don't start or end tweet with quotation marks. 
            Idea: "
        };

        public static readonly string[] OpenAIPromptTweetHashtags = new[]
{
            @"You are going to be hashtag writer for tweets.
            Your main goal is to write hashtags for tweet. Hashtags should be popular. 
            Max amount of hashtags is 5. Don't write any comments to hashtags, only hashtags.
            Words mustn't contain '#'. After each hashtag make one space.                
            My tweet: "
        };
    }
}
