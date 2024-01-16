using ReditPostTracker.Managers;
using ReditPostTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReditPostTracker.Services
{
    public class RedditPostSelector
    {
        //Class accepts Interface  IRedditManager for Dependency Injection , decoupling , testatbility using Moq Object for Reddit Posts,Ease of COnfiguration,
        private readonly IRedditManager redditManager;

        public RedditPostSelector(IRedditManager redditManager)
        {
            this.redditManager = redditManager;
        }
        //This method will call posts for maximum upvotes count and the user who did most post in a given time Frame (We are targeting from the past one hour)
        public void AnalyzeRedditPosts(string subReddit)
        {
            try
            {
                List<DigestedRedditPost> posts = new();
                posts.AddRange(redditManager.GetLastHourPosts(subReddit));
                PrintPostWithMaximumUpvotes(posts);
                PrintMostCommonAuthor(posts);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                // Log the exception instead of writing to the console directly
            }
        }
        //Method for post with maxiumum upvotes
        private void PrintPostWithMaximumUpvotes(List<DigestedRedditPost> posts)
        {
            try
            {
                int maximum = 0;
                string valuePost = null;
                string author = "";
                foreach (DigestedRedditPost post in posts)
                {
                    if (post.UpVotes > maximum)
                    {
                        maximum = post.UpVotes;
                        valuePost = post.URL;
                        author = post.Author;
                    }
                }

                Console.WriteLine($"Post with Maximum Upvotes: {valuePost} ::::::::::: {author}");
                Console.WriteLine($"Count : {maximum}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        //Method of a user with most posts in one hour.
        private void PrintMostCommonAuthor(List<DigestedRedditPost> posts)
        {
            try
            {
                if (posts.Count == 0)
                {
                    Console.WriteLine($"We dont have any posts for the given timeframe for this Subreddit");
                }

                var mostOccurredAuthor = posts.GroupBy(x => x.Author)
                                              .OrderByDescending(g => g.Count())
                                              .Select(g => g.Key)
                                              .FirstOrDefault();

                if (mostOccurredAuthor != null)
                {
                    Console.WriteLine($"The User Agent with the maximum occurrence is: {mostOccurredAuthor}");
                }
                else
                {
                    Console.WriteLine("The list is empty.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

    }

}
