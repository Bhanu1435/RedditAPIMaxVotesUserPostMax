using ReditPostTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReditPostTracker
{
    //Created Interface for IRedditManager for making code decoupled and testable.
    public interface IRedditManager
    {
        List<DigestedRedditPost> GetLastHourPosts(string subReddit);
    }
}
