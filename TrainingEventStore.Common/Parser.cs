using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace TrainingEventStore.Common
{
    public static class Parser
    {
        public static EventData GetEventData(Object item)
        {
            var jsonString = JsonConvert.SerializeObject(item, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
            var jsonPayload = Encoding.UTF8.GetBytes(jsonString);
            string EventName = item.GetType().Name;

            return new EventData(Guid.NewGuid(), EventName, true, jsonPayload, null);
        }

        public static T ParseJson<T>(this RecordedEvent data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var value = Encoding.UTF8.GetString(data.Data);

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
