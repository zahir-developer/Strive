using MediatR;

namespace Strive.Library.Configuration.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}