using System;
using EventStore.ClientAPI;

namespace TrainingEventStore.Common
{
    public static class EventStoreConnect
    {
        public static IEventStoreConnection CreateConnection()
        {
            var connectionSettings = ConnectionSettings.Create().DisableTls().Build();
            var connection = EventStoreConnection.Create(connectionSettings, new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().Wait();
            return connection;
        }
    }
}
