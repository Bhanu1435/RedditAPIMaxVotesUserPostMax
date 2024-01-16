using Reddit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReditPostTracker.Models
{
    public class DigestedRedditPost
    {
        public DigestedRedditPost()
        {

        }
        //These are variables provided by Reddit Json object created a model class for that.
        public DigestedRedditPost(Post post)
        {
            Title = post.Title;
            Author = post.Author;
            UpVotes = post.UpVotes;
            URL = "https://reddit.com" + post.Permalink;
            Content = post.Listing.SelfText;
            SubReddit = post.Subreddit;
            PostedDate = post.Created;
        }

        [JsonPropertyName("Title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("Author")]
        public string Author { get; set; } = string.Empty;
        [JsonPropertyName("UpVotes")]
        public int UpVotes { get; set; } = 0;
        [JsonPropertyName("URL")]
        public string URL { get; set; } = string.Empty;
        [JsonPropertyName("Content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("SubReddit")]
        public string SubReddit { get; set; } = string.Empty;
        [JsonPropertyName("PostedDate")]
        public DateTime PostedDate { get; set; } = DateTime.Now;
    }
}
