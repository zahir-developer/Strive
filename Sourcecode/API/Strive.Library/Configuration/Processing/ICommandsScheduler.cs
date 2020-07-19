using System.Threading.Tasks;
using MediatR;
using Strive.Library.Configuration.Commands;

namespace Strive.Library.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}