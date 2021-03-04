using System;
using EventStore.ClientAPI;
using System.Net;

namespace TrainingEventStore.Common
{
    public static class EventStoreConnect
    {
        const int DEFAULTPORT = 1113;
      

        public static IEventStoreConnection CreateConnection()
        {
            var connectionSettings = ConnectionSettings.Create().DisableTls().Build();
            var connection = EventStoreConnection.Create(connectionSettings, new IPEndPoint(IPAddress.Loopback, DEFAULTPORT));
            connection.ConnectAsync().Wait();
            return connection;
        }
    }
}
