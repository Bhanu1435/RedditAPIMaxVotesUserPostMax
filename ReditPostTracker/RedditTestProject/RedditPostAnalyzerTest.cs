using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Reddit.Controllers;
using ReditPostTracker.Managers;
using ReditPostTracker.Models;
using ReditPostTracker.Services;
using Xunit;

namespace RedditTestProject
{
    public class RedditPostAnalyzerTest
    {
        // Example test method for the AnalyzeRedditPosts method
        [Fact]
        public void AnalyzeRedditPosts_ShouldPrintResultsTest()
        {
            // Arrange: Setup necessary dependencies using Mocking (Moq)
            var redditManagerMock = new Mock<RedditManager>();
            var postSelector = new RedditPostSelector(redditManagerMock.Object);

            // Prepare test data
            var testPosts = new List<DigestedRedditPost>
        {
            new DigestedRedditPost { Title = "Test Post 1", UpVotes = 10, Author = "User1" ,URL = "wnm.com"},
            new DigestedRedditPost { Title = "Test Post 2", UpVotes = 15, Author = "User2" ,URL = "wnm.com"},
            new DigestedRedditPost { Title = "Test Post 3", UpVotes = 25, Author = "User3", URL = "wnm.com" },
            new DigestedRedditPost { Title = "Test Post 4", UpVotes = 3, Author = "User4" , URL = "wnm.com"},
            new DigestedRedditPost { Title = "Test Post 5", UpVotes = 67, Author = "User5" ,URL = "wnm.com"},
            new DigestedRedditPost { Title = "Test Post 6", UpVotes = 72, Author = "User6" ,URL = "wnm.com"},
            new DigestedRedditPost { Title = "Test Post 7", UpVotes = 72, Author = "User1" ,URL = "wnm.com"}
            // Add more test data as needed
        };

            // Mock the GetLastHourPosts method to return the test data
            redditManagerMock.Setup(rm => rm.GetLastHourPosts(It.IsAny<string>())).Returns(testPosts);

            // Act: Call the method going to be test
            postSelector.AnalyzeRedditPosts("testSubreddit");
            Console.WriteLine("TODO: Add assertions for expected console output");

            
        }

        
    }
}