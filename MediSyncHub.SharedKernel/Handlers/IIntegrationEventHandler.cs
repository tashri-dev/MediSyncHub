namespace MediSyncHub.SharedKernel.Handlers;

public interface IIntegrationEventHandler<in TEvent>
        where TEvent : class
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}