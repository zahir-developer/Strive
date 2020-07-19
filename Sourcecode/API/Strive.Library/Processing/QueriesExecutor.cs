using System.Threading.Tasks;
using Autofac;
using MediatR;
using Strive.Library.Configuration.Queries;
//using SampleProject.Application;
//using SampleProject.Application.Configuration.Queries;

namespace Strive.Library.Processing
{
    public static class QueriesExecutor
    {
        public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            using (var scope = CompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}