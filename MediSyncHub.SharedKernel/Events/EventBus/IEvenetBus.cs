using MediSyncHub.SharedKernel.Handlers;

namespace MediSyncHub.SharedKernel.Events.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T @event) where T : class;
    void Subscribe<T, TH>() where T : class where TH : IIntegrationEventHandler<T>;
}
