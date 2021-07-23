using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeter.Modules.Message
{
    public partial class GroupViewController
    {
        List<string> groups = new();

        public GroupViewController()
        {
            groups.Add("OM Detailers");
            groups.Add("OM Washers");
            groups.Add("MS Washers");
            groups.Add("MS Detailers");
            groups.Add("Old Milton Employees");
            groups.Add("Main Street Employees");
            groups.Add("Holcomb Bridge Employees");
        }

        Task GetMessageGroups()
        {
            return Task.CompletedTask;
        }

        async Task SearchGroup(string keyword)
        {
            await Task.CompletedTask;
        }

        async Task OnRefersh()
        {
            //TODO refresh data

            await Task.Delay(2000);
            refreshControl.EndRefreshing();
        }
    }
}