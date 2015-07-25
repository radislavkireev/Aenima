﻿using System;
using System.Collections.Generic;
using Aenima.EventStore;
using Aenima.System;
using Aenima.System.Extensions;
using Newtonsoft.Json;

namespace Aenima.JsonNet
{
    public class JsonNetEventSerializer : IEventSerializer
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            TypeNameHandling  = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver  = EventContractResolver.Instance
        };

        public NewStreamEvent ToNewStreamEvent<TEvent>(TEvent e, IDictionary<string, object> headers = null) where TEvent : class, IEvent
        {
            var jsonData     = JsonConvert.SerializeObject(e, Settings);
            var jsonMetadata = headers != null 
                ? JsonConvert.SerializeObject(headers, Settings)
                : string.Empty;

            var eventId = headers != null && headers.ContainsKey("Id")
                ? headers["Id"].ToGuidOrDefault(SequentialGuid.New())
                : SequentialGuid.New();

            return new NewStreamEvent(
                id      : eventId, 
                type    : e.GetType().Name, 
                data    : jsonData, 
                metadata: jsonMetadata);
        }

        public TEvent FromStreamEvent<TEvent>(StreamEvent streamEvent, out IDictionary<string, object> headers) where TEvent : class, IEvent
        {
            headers =
                JsonConvert.DeserializeObject(
                    streamEvent.Metadata,
                    typeof(IDictionary<string, object>),
                    Settings) as IDictionary<string, object>;

            return JsonConvert.DeserializeObject(
                value   : streamEvent.Data, 
                type    : typeof(TEvent), 
                settings: Settings) as TEvent;
        }
    }

    //public class JsonNetEventSerializer : IEventSerializer
    //{
    //    public static readonly JsonSerializerSettings ToNewStreamEventSerializerSettings = new JsonSerializerSettings
    //    {
    //        TypeNameHandling = TypeNameHandling.None,
    //        NullValueHandling = NullValueHandling.Ignore,
    //        ContractResolver = ToNewStreamEventContractResolver.Instance
    //    };

    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        TypeNameHandling = TypeNameHandling.None,
    //        NullValueHandling = NullValueHandling.Ignore,
    //        ContractResolver = EventContractResolver.Instance
    //    };

    //    public NewStreamEvent ToNewStreamEvent<TEvent>(TEvent e, IDictionary<string, object> headers = null) where TEvent : class, IEvent
    //    {
    //        var jsonData = JsonConvert.SerializeObject(e, ToNewStreamEventSerializerSettings);
    //        var jsonMetadata = JsonConvert.SerializeObject(headers, ToNewStreamEventSerializerSettings);

    //        return new NewStreamEvent((Guid)headers["Id"], e.GetType().Name, jsonData, jsonMetadata);
    //    }

    //    public TEvent FromStreamEvent<TEvent>(StreamEvent streamEvent, out IDictionary<string, object> headers) where TEvent : class, IEvent
    //    {
    //        headers =
    //            JsonConvert.DeserializeObject(
    //                streamEvent.Data,
    //                typeof(IDictionary<string, object>),
    //                Settings) as IDictionary<string, object>;

    //        var eventClrType = headers?["EventClrType"]?.ToString();

    //        if(eventClrType == null)
    //        {
    //            throw new MissingFieldException($"Failed to find event CLR Type for stream event with Type {streamEvent.Type}! Invalid headers!");
    //        }

    //        return JsonConvert.DeserializeObject(
    //            value: streamEvent.Data,
    //            type: Type.GetType(eventClrType),
    //            settings: Settings) as TEvent;
    //    }
    //}
}