using Microsoft.Extensions.Configuration;
using OpenAI_API.Chat;
using OpenAI_API;
using Suvorov.LNU.TwitterClone.Core.Constants;
using OpenAI_API.Moderation;

namespace Suvorov.LNU.TwitterClone.OpenAI
{
    public class TweetsGenerator
    {
        private readonly string apiKey;

        public TweetsGenerator(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<string> GenerateTweetText(string userPrompt)
        {
            var api = new OpenAIAPI(apiKey);

            var chatRequest = new ChatRequest
            {
                Messages = new ChatMessage[]
                {
                    new ChatMessage(ChatMessageRole.System, ProjectConstants.OpenAIPromptTweetText[0] + userPrompt),
                }
            };

            var chatResult = await api.Chat.CreateChatCompletionAsync(chatRequest);

            string? generatedTweet = chatResult?.Choices?[0]?.Message?.Content;

            if (string.IsNullOrEmpty(generatedTweet))
            {
                throw new Exception("Failed to generate a tweet.");
            }

            int hashtagIndex = generatedTweet.IndexOf('#');
            if (hashtagIndex >= 0)
            {
                generatedTweet = generatedTweet.Substring(0, hashtagIndex);
            }

            return generatedTweet;
        }

        public async Task<List<string>> GenerateTweetHashtags(string userTweet)
        {
            var api = new OpenAIAPI(apiKey);

            var chatRequest = new ChatRequest
            {
                Messages = new ChatMessage[]
                {
                    new ChatMessage(ChatMessageRole.System, ProjectConstants.OpenAIPromptTweetHashtags[0] + userTweet),
                }
            };

            var chatResult = await api.Chat.CreateChatCompletionAsync(chatRequest);

            string? generatedTweetHashtags = chatResult?.Choices?[0]?.Message?.Content;

            if (string.IsNullOrEmpty(generatedTweetHashtags))
            {
                throw new Exception("Failed to generate hashtags for tweet.");
            }

            List<string> hashtagList = generatedTweetHashtags.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < hashtagList.Count; i++)
            {
                string cleanedHashtag = hashtagList[i].TrimStart('#');
                hashtagList[i] = cleanedHashtag;
            }

            return hashtagList;
        }

    }
}