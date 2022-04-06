using Strive.Common;

namespace Strive.BusinessLogic.Job
{
    public interface IJobBpl
    {
        Result GetPrintJobDetail(int JobId);
    }
}
