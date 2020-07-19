using System;
using System.Threading.Tasks;

namespace Strive.Library.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
