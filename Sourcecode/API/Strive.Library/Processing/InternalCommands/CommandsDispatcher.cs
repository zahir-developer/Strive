using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
//using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
//using SampleProject.Application.Configuration.Processing;
//using SampleProject.Application.Customers;
//using Strive.Library.Database;

namespace Strive.Library.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        //private readonly OrdersContext _ordersContext;

        public CommandsDispatcher(
            IMediator mediator 
            //,OrdersContext ordersContext
            )
        {
            this._mediator = mediator;
           // this._ordersContext = ordersContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            //var internalCommand = default;// await this._ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            //Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(internalCommand.Type);
            //dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            //internalCommand.ProcessedDate = DateTime.UtcNow;

            await this._mediator.Send(null);
        }
    }
}