using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Reddit;
using Reddit.AuthTokenRetriever;
using Reddit.Controllers;
using Reddit.Inputs;
using Reddit.Inputs.Search;
using ReditPostTracker.Models;

namespace ReditPostTracker.Managers
{
    public class RedditManager:IRedditManager
    {
        //Reddit API used Assembly Reddit.NET, Version=1.5.2.0 and Reddit.NET is a .NET Standard library that provides easy access to the Reddit API with virtually no boilerplate code required.
        //Also made used Reddit for AUthorizing user with a if and secret key
        private readonly RedditClient _redditClient;

        //The RedditManager class also has a parameterless constructor to support dependency injection.
        public RedditManager(string appID, string appSecret)
        {
            _redditClient = AuthorizeUser(appID, appSecret);
        }

        //Parameterless constructor for Dependency Injection
        public RedditManager()
        {

        }
        private RedditClient? AuthorizeUser(string appId, string appSecret)
        {
            try
            {
                int port = 8080;
                AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, port, "localhost", appSecret: appSecret);

                authTokenRetrieverLib.AwaitCallback();
                OpenBrowser(authTokenRetrieverLib.AuthURL());

                while (string.IsNullOrWhiteSpace(authTokenRetrieverLib.RefreshToken))
                {
                    // You can add some delay or sleep here to avoid unnecessary CPU usage
                }

                authTokenRetrieverLib.StopListening();

                if (authTokenRetrieverLib.AccessToken == null)
                {
                    Console.WriteLine("Bad Request or Error occurred during authorization");
                    return null;
                }

                return new RedditClient(appId, authTokenRetrieverLib.RefreshToken, appSecret, authTokenRetrieverLib.AccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public virtual List<DigestedRedditPost> GetLastHourPosts(string subReddit)
        {
            try
            {
                DateTime oneHourAgo = DateTime.Now.AddHours(-1);

                IEnumerable<Post> posts = _redditClient.Subreddit(subReddit)
                    .Posts.GetNew(new CategorizedSrListingInput(limit: int.MaxValue));

                List<DigestedRedditPost> digestedRedditPosts = new List<DigestedRedditPost>();

                foreach (Post post in posts)
                {
                    try
                    {
                        if (post.Created >= oneHourAgo)
                        {
                            DigestedRedditPost digestedPost = new DigestedRedditPost(post);
                            digestedRedditPosts.Add(digestedPost);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing post: {ex.Message}");
                    }
                }

                return digestedRedditPosts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<DigestedRedditPost>();
            }
        }
        private static void OpenBrowser(string authUrl, string browserPath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe")
        {
            try
            {
                Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });
            }
            catch (System.ComponentModel.Win32Exception)
            {
                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(browserPath)
                    {
                        Arguments = authUrl
                    };
                    Process.Start(processStartInfo);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    Console.WriteLine("Failed to open the browser.");
                }
            }
            catch (System.Net.WebException ex)
            {
                {
                    Console.WriteLine($"WebException: {ex.Message}");
                    if (ex.Response is HttpWebResponse response)
                    {
                        Console.WriteLine($"HTTP Status Code: {response.StatusCode}");
                        Console.WriteLine($"Status Description: {response.StatusDescription}");
                    }
                }
            }
        }

    }
}
