﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NServiceBus;
using static System.String;

namespace Aenima.NServiceBus
{
    public class NServiceBusEventDispatcher : IEventDispatcher
    {
        private readonly IBus bus;

        public NServiceBusEventDispatcher(IBus bus)
        {
            this.bus = bus;
        }

        public Task Dispatch<T>(T e, IDictionary<string, object> headers = null) where T : class, IEvent
        {
            if(headers != null) {
                foreach(var header in headers) {
                    this.bus.SetMessageHeader(e, $"Aenima-{header.Key}", header.Value.ToString());
                }
            }

            this.bus.Publish(e);

            return Task.FromResult(0);
        }
    }

    public static class BusExtensions
    {
        public static string GetAenimaHeader(this IBus bus, string key)
        {
            var header = $"Aenima-{key}";

            return bus.CurrentMessageContext.Headers.ContainsKey(header)
                ? bus.CurrentMessageContext.Headers[header]
                : Empty;
        }
    }
}