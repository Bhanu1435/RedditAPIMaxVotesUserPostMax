using Reddit.Controllers;
using ReditPostTracker.Managers;
using ReditPostTracker.Models;
using ReditPostTracker.Services;


public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            Console.WriteLine("Welcome to track Reddit Posts");

            //Instead of reading appId & Secret Key we could store information on AzureKey walt as well
            //, but here I am providing facilty for user prompts.
            //I will catch the data for last hour of max upvotes and user who did most post in that time for a particular Subreddit.

            string appID = "";
            string appSecret = "";
            string subReddit = "";
            Console.WriteLine("Please enter appId for Reddit APP");
            appID = Console.ReadLine();
            Console.WriteLine("Please enter App Secret Key");
            appSecret = Console.ReadLine();
            Console.WriteLine("Please enter subReddit");
            subReddit = Console.ReadLine();

            // Validate user input for Null & White Space
            if (string.IsNullOrWhiteSpace(appID) || string.IsNullOrWhiteSpace(appSecret) || string.IsNullOrWhiteSpace(subReddit))
            {
                Console.WriteLine("Invalid input. Please provide values for App ID, App Secret, and Subreddit.");
                return; // Exit the program or prompt the user again as needed
            }

            //Calling Reddit Manager Constructor and Authorization for entering 

            RedditManager redditManager = new(appID, appSecret);
            RedditPostSelector postSelector = new RedditPostSelector(redditManager);
            postSelector.AnalyzeRedditPosts(subReddit);
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message.ToString());
        }
    }
}