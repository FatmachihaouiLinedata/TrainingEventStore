using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingEventStore.Common;

namespace TrainingEventStore.InventoryManager
{
    // in this class : subscription to salestream (to see products and quatities of sales **
    public class InventoryManagerServices
    {
       
        public InventoryManagerServices()
        {
        }

     
        //Read from salesStream


        public void SubscribetoSales()
        {
            EventStoreConnect.CreateConnection().SubscribeToStreamFrom(
            stream: "SalesStream",
            lastCheckpoint: StreamPosition.Start,
            settings: CatchUpSubscriptionSettings.Default,
             eventAppeared: (sub, evt)
             => Console.Out.WriteLineAsync("Event appeared"),
             liveProcessingStarted: sub
              => Console.WriteLine("Processing live"),
          subscriptionDropped: (sub, reason, exception)
         => Console.WriteLine($"Subscription dropped: {reason}")
            );

        }
        public void eventAppeared(EventStoreCatchUpSubscription sub, ResolvedEvent evt)
        {
            var newSale = Parser.ParseJson<SaleAchievedEvent>(evt.Event);
            Console.WriteLine("new sale added with the following information");

            Console.WriteLine(newSale.ProductName + "    " + newSale.Quantity);
        }
    
       
    }
}
