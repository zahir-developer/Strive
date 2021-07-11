using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class RecentMessageViewController
    {
        List<string> recentMessageHistory = new();

        public RecentMessageViewController()
        {
            recentMessageHistory.Add("");
            recentMessageHistory.Add("");
            recentMessageHistory.Add("");
            recentMessageHistory.Add("");
            recentMessageHistory.Add("");
        }

        Task GetRecentsMessageHistory()
        {
            return Task.CompletedTask;
        }
    }
}
