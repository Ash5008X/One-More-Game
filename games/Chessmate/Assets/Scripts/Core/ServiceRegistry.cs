using System;
using System.Collections.Generic;

namespace Chessmate.Core
{
    /// <summary>
    /// Stores references to all game services.
    /// </summary>
    public static class ServiceRegistry
    {
        private static readonly Dictionary<Type, object> services = new();

        public static void Register<T>(T service)
        {
            Type type = typeof(T);

            if (services.ContainsKey(type))
            {
                throw new Exception($"{type.Name} is already registered.");
            }

            services.Add(type, service);
        }

        public static T Get<T>()
        {
            Type type = typeof(T);

            if (!services.TryGetValue(type, out object service))
            {
                throw new Exception($"{type.Name} is not registered.");
            }

            return (T)service;
        }

        public static void Clear()
        {
            services.Clear();
        }
    }
}