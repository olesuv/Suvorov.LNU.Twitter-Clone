using Microsoft.AspNetCore.Routing;
using Suvorov.LNU.TwitterClone.Algorithms;

namespace Suvorov.LNU.TwitterClone.Test
{
    [TestClass]
    public class GeneratedTweetsCorrectionTests : TestBase
    {
        [TestMethod]
        public async Task ExtractTagsFromPostTest()
        {
            string userPost = "Hello #world! This is a #test.";

            List<string> tags = await GeneratedTweetsCorrection.ExtractTagsFromPost(userPost);

            foreach (string tag in tags) 
            {
                Console.WriteLine(tag);
            }

            Assert.AreEqual(2, tags.Count);
            CollectionAssert.Contains(tags, "world!");
            CollectionAssert.Contains(tags, "test.");
        }

        [TestMethod]
        public async Task RemoveTagsFromPostTest()
        {
            string userPost = "Hello #world! This is a #test.";

            string cleanedPost = await GeneratedTweetsCorrection.RemoveTagsFromPost(userPost);

            Console.WriteLine(cleanedPost);

            Assert.AreEqual("Hello This is a", cleanedPost);
        }

        [TestMethod]
        public async Task RemoveQuotesTest()
        {
            string tweetWithQuotes = "\"This is a quoted tweet.\"";
            string tweetWithoutQuotes = "This is a tweet without quotes.";

            string resultWithQuotes = await GeneratedTweetsCorrection.RemoveQuotes(tweetWithQuotes);
            string resultWithoutQuotes = await GeneratedTweetsCorrection.RemoveQuotes(tweetWithoutQuotes);

            Assert.AreEqual("This is a quoted tweet.", resultWithQuotes);
            Assert.AreEqual(tweetWithoutQuotes, resultWithoutQuotes);
        }
    }
}
