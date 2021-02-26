using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using TrainingEventStore.Common;
using TrainingEventStore.InventoryManager;

namespace TrainingEventStore.Director
{
    public class DirectorServices
    {
        private SaleServices saleServices = new SaleServices();
        public DirectorServices()
        {
            
        }

        public void SubscribeToSaleEvent()
        {
            EventStoreConnect.CreateConnection().SubscribeToStreamFrom(
                stream: "SalesStream",
                lastCheckpoint: StreamPosition.Start,
                settings: CatchUpSubscriptionSettings.Default,
                eventAppeared: (sub, evt)
                    => eventAppeared(sub, evt),
                liveProcessingStarted: sub
                    => Console.WriteLine("Processing live"),
                subscriptionDropped: (sub, reason, exception)
                    => Console.WriteLine($"Subscription dropped: {reason}")
            );

        }

        public void eventAppeared(EventStoreCatchUpSubscription sub, ResolvedEvent evt)
        {
            saleServices.ReadSales();
        }
    }
}
