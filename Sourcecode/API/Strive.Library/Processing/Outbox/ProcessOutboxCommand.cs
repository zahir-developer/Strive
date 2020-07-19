using MediatR;
using Strive.Library.Configuration.Commands;
//using SampleProject.Application;
//using SampleProject.Application.Configuration.Commands;

namespace Strive.Library.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}