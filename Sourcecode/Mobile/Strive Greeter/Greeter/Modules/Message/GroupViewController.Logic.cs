using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class GroupViewController
    {
        List<string> groups = new();

        public GroupViewController()
        {
            groups.Add("");
            groups.Add("");
            groups.Add("");
            groups.Add("");
            groups.Add("");
            groups.Add("");
        }

        Task GetMessageGroups()
        {
            return Task.CompletedTask;
        }

        async Task SearchGroup(string keyword)
        {
            await Task.CompletedTask;
        }
    }
}