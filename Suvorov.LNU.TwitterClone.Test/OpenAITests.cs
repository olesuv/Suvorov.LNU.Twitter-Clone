using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Core.Constants;
using Suvorov.LNU.TwitterClone.Models.Configuration;
using Suvorov.LNU.TwitterClone.OpenAI;
using System.Configuration;
using System.Reflection.Metadata;

namespace Suvorov.LNU.TwitterClone.Test
{
    [TestClass]
    public class OpenAITests : TestBase
    {
        readonly string OpenAI_API_KEY;

        IOptions<AppConfig> _configuration;

        public OpenAITests()
        {
            _configuration = ResolveService<IOptions<AppConfig>>();
            OpenAI_API_KEY = _configuration.Value.OpenAI_API_KEY;
        }

        [TestMethod]
        public async Task GenerateTweetTextTest()
        {
            var tweetsGenerator = new TweetsGenerator(OpenAI_API_KEY);

            var userPrompt = "Joke about .NET developer.";

            var tweet = await tweetsGenerator.GenerateTweetText(userPrompt);

            Console.WriteLine("Generated tweet: ");
            Console.WriteLine(tweet);

            Assert.IsFalse(string.IsNullOrEmpty(tweet), "Generated tweet should not be empty or null.");
        }

        [TestMethod]
        public async Task GenerateTweetHashtagsTest()
        {
            var tweetsGenerator = new TweetsGenerator(OpenAI_API_KEY);
            string userTweet = "Joke about .NET developer.";

            List<string> result = await tweetsGenerator.GenerateTweetHashtags(userTweet);

            Console.WriteLine("Generated hashtags:");
            foreach (var hashtag in result)
            {
                Console.WriteLine(hashtag);
            }

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<List<string>>(result);
            Assert.IsTrue(result.Count > 0);
        }

    }
}
