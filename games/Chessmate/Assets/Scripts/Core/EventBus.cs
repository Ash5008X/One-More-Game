using System;
using System.Collections.Generic;

namespace Chessmate.Core
{
    /// <summary>
    /// Simple event system used for communication between game systems.
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> events = new();

        public static void Subscribe<T>(Action<T> listener)
        {
            Type type = typeof(T);

            if (events.TryGetValue(type, out Delegate existing))
            {
                events[type] = Delegate.Combine(existing, listener);
            }
            else
            {
                events[type] = listener;
            }
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            Type type = typeof(T);

            if (!events.TryGetValue(type, out Delegate existing))
                return;

            Delegate current = Delegate.Remove(existing, listener);

            if (current == null)
                events.Remove(type);
            else
                events[type] = current;
        }

        public static void Publish<T>(T eventData)
        {
            Type type = typeof(T);

            if (events.TryGetValue(type, out Delegate existing))
            {
                ((Action<T>)existing)?.Invoke(eventData);
            }
        }

        public static void Clear()
        {
            events.Clear();
        }
    }
}