using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class GroupViewController
    {
        List<string> groups = new();
        List<string> searchedGroups;

        public GroupViewController()
        {
            groups.Add("OM Detailers");
            groups.Add("OM Washers");
            groups.Add("MS Washers");
            groups.Add("MS Detailers");
            groups.Add("Old Milton Employees");
            groups.Add("Main Street Employees");
            groups.Add("Holcomb Bridge Employees");

            searchedGroups = groups;
        }

        Task GetMessageGroups()
        {
            return Task.CompletedTask;
        }

        async Task SearchGroup(string keyword)
        {
            searchedGroups = await Task.Run(() => groups.Where(name => name.Contains(keyword)).ToList());
        }

        async Task OnRefersh()
        {
            //TODO refresh data

            await Task.Delay(2000);
            refreshControl.EndRefreshing();
        }
    }
}