using System.Threading.Tasks;

namespace Strive.Library.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}