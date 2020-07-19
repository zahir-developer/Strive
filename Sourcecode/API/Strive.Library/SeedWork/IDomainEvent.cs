using System;
using MediatR;

namespace Strive.Library.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}