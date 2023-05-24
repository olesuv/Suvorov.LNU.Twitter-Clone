using Microsoft.AspNetCore.Routing;
using Suvorov.LNU.TwitterClone.Algorithms;

namespace Suvorov.LNU.TwitterClone.Test
{
    [TestClass]
    public class DetectTagsInPostTests : TestBase
    {
        [TestMethod]
        public async Task ExtractTagsFromPostTest()
        {
            string userPost = "Hello #world! This is a #test.";

            List<string> tags = await DetectTagsInPost.ExtractTagsFromPost(userPost);

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

            string cleanedPost = await DetectTagsInPost.RemoveTagsFromPost(userPost);

            Console.WriteLine(cleanedPost);

            Assert.AreEqual("Hello This is a", cleanedPost);
        }
    }
}
