using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediSyncHub.SharedKernel.Events.EventBus;
public class InMemoryEventBus(
    IServiceProvider serviceProvider,
    ILogger<InMemoryEventBus> logger)
    : IEventBus
{
    private readonly Dictionary<string, List<(Type HandlerType, Type EventType)>> _handlers = new();

    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken) where T : class
    {
        var eventName = @event.GetType().Name;
        logger.LogInformation("Publishing event: {EventName}", eventName);

        if (_handlers.ContainsKey(eventName))
        {
            using var scope = serviceProvider.CreateScope();
            var registeredHandlers = _handlers[eventName];

            var tasks = new List<Task>();

            foreach (var (handlerType, eventType) in registeredHandlers)
            {
                try
                {
                    var handler = scope.ServiceProvider.GetService(handlerType);
                    if (handler == null)
                    {
                        logger.LogWarning("Handler not found for type: {HandlerType}", handlerType.Name);
                        continue;
                    }

                    // Get the generic HandleAsync method
                    var method = handlerType
                        .GetMethod(nameof(IIntegrationEventHandler<T>.HandleAsync))!;

                    // Invoke the method with the event
                    var task = (Task)method.Invoke(
                        handler, 
                        new object[] { @event, cancellationToken })!;

                    tasks.Add(task);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, 
                        "Error while handling event {EventName} with handler {HandlerType}", 
                        eventName, 
                        handlerType.Name);
                }
            }

            if (tasks.Any())
            {
                await Task.WhenAll(tasks);
            }
        }
        else
        {
            logger.LogWarning("No handlers registered for event: {EventName}", eventName);
        }
    }

    public void Subscribe<T, TH>()
        where T : class
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);
        var eventType = typeof(T);

        logger.LogInformation(
            "Subscribing to event {EventName} with handler {HandlerType}",
            eventName,
            handlerType.Name);

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers[eventName] = new List<(Type HandlerType, Type EventType)>();
        }

        if (_handlers[eventName].Any(h => h.HandlerType == handlerType))
        {
            logger.LogWarning(
                "Handler Type {HandlerType} already registered for '{EventName}'", 
                handlerType.Name, 
                eventName);
        }
        else
        {
            _handlers[eventName].Add((handlerType, eventType));
        }
    }
}