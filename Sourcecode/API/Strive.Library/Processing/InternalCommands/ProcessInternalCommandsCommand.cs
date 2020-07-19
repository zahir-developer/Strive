using MediatR;
using Strive.Library.Configuration.Commands;
//using SampleProject.Application;
//using SampleProject.Application.Configuration.Commands;
using Strive.Library.Processing.Outbox;

namespace Strive.Library.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}