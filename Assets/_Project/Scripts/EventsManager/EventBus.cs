using System;
using System.Collections.Generic;

namespace gishadev.walkingSimulator.EventsManager
{
    public class EventBus : IEventBus
    {
        // Dictionary to store handlers for each event type
        private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new();

        public void Subscribe<T>(Action<T> handler) where T : GameEvent
        {
            var eventType = typeof(T);
            if (!_eventHandlers.ContainsKey(eventType))
                _eventHandlers[eventType] = new List<Delegate>();

            _eventHandlers[eventType].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : GameEvent
        {
            var eventType = typeof(T);
            if (!_eventHandlers.ContainsKey(eventType))
                return;

            _eventHandlers[eventType].Remove(handler);

            // Clean up empty lists
            if (_eventHandlers[eventType].Count == 0)
                _eventHandlers.Remove(eventType);
        }

        public void Publish<T>(T gameEvent) where T : GameEvent
        {
            var eventType = typeof(T);
            if (!_eventHandlers.ContainsKey(eventType))
                return;

            // Create a copy to avoid issues if handlers subscribe/unsubscribe during event processing
            var handlers = new List<Delegate>(_eventHandlers[eventType]);

            foreach (var handler in handlers)
            {
                if (handler is Action<T> typedHandler)
                    typedHandler(gameEvent);
            }
        }
    }
}