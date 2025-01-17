using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace MediSyncHub.SharedKernel.Events;

public class InMemoryEventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, List<Type>> _handlers;

    public InMemoryEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _handlers = new Dictionary<string, List<Type>>();
    }

    public Task PublishAsync<T>(T @event) where T : class
    {
        var eventName = @event.GetType().Name;

        if (_handlers.ContainsKey(eventName))
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerTypes = _handlers[eventName];

            var tasks = handlerTypes.Select(handlerType =>
            {
                var handler = scope.ServiceProvider.GetService(handlerType);
                return ((IIntegrationEventHandler<T>)handler).HandleAsync(@event);
            });

            return Task.WhenAll(tasks);
        }

        return Task.CompletedTask;
    }

    public void Subscribe<T, TH>()
        where T : class
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers[eventName] = new List<Type>();
        }

        _handlers[eventName].Add(handlerType);
    }
}
