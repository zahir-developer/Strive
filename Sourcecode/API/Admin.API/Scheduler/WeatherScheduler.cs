using Quartz;
using System.Threading.Tasks;

namespace Admin.API.Scheduler
{
    [DisallowConcurrentExecution]
    public class WeatherScheduler : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
