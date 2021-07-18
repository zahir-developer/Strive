using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class RecentMessageViewController
    {
        List<string> recentMessageHistory = new();

        public RecentMessageViewController()
        {
            recentMessageHistory.Add("Brittany Rose");
            recentMessageHistory.Add("OM Detailers");
            recentMessageHistory.Add("Peter Parker");
            recentMessageHistory.Add("Daniel Steel");
            recentMessageHistory.Add("Old Milton Employees");
        }

        Task GetRecentsMessageHistory()
        {
            return Task.CompletedTask;
        }
    }
}
