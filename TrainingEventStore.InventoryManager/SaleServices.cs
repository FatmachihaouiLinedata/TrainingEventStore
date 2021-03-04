using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using TrainingEventStore.Common;
using TrainingEventStore.Common.Products;

namespace TrainingEventStore.InventoryManager
{
    public class SaleServices
    {
        public SaleServices()
        {
           
        }
        public void AddSale()
        {
            do
            {
                
                Console.WriteLine("write product name :");
                string productName = Console.ReadLine();
                Console.WriteLine("write quantity:");
                string quantity = Console.ReadLine();
                int qte = Convert.ToInt32(quantity);

                SaleAchievedEvent saleAchieved = new SaleAchievedEvent(Guid.NewGuid(), productName, qte);
                var evtdatatype = Parser.GetEventData(saleAchieved);
                EventStoreConnect.CreateConnection().AppendToStreamAsync("SalesStream", ExpectedVersion.Any, evtdatatype).Wait();
            } while (!Console.ReadKey().KeyChar.Equals('q'));

        }
        public void SubscribeToProductEvent()
        {
            EventStoreConnect.CreateConnection().SubscribeToStreamFrom(
                stream: "ProductStream",
                lastCheckpoint: StreamPosition.Start,
                settings: CatchUpSubscriptionSettings.Default,
                eventAppeared: (sub, evt)
                => eventAppeared(sub, evt),
                liveProcessingStarted: sub
                 => Console.WriteLine("Processing live"),
                 subscriptionDropped: (sub, reason, exception)
                => Console.WriteLine($"Subscription dropped: {reason}")
                ); ;
        }

        public void eventAppeared(EventStoreCatchUpSubscription sub, ResolvedEvent evt)
        {
            var newproductdded = Parser.ParseJson<ProductEventCreated>(evt.Event);
            Console.WriteLine("new product added with the following information");

            Console.WriteLine(newproductdded.ProductName + "    " + newproductdded.Quantity+ "    "+ newproductdded.Price +"       ");
               
        }

    }
}
