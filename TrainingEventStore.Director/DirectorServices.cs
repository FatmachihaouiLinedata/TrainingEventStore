using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System;
using System.Collections.Generic;
using TrainingEventStore.Common;
using TrainingEventStore.Common.Products;
using TrainingEventStore.InventoryManager;

namespace TrainingEventStore.Director
{
    public class DirectorServices
    {
        private List<Product> products = new List<Product>();
        private decimal TotalAmmount = 0;

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
                    => eventAppeared(evt),
                liveProcessingStarted: sub
                    => Console.WriteLine("Processing live"),
                subscriptionDropped: (sub, reason, exception)
                    => Console.WriteLine($"Subscription dropped: {reason}")
            );

        }
        public void eventAppeared(ResolvedEvent evt)
        {
            SaleAchievedEvent saleAchievedEvent = Parser.ParseJson<SaleAchievedEvent>(evt.Event);
            ReadProductsAsync();
            foreach (var product in products)
            {
                if(product.ProductName.Equals(saleAchievedEvent.ProductName))
                {
                    TotalAmmount = TotalAmmount + (product.Price * saleAchievedEvent.Quantity);
                }
            }
            Console.WriteLine("total ammount of sales: "+TotalAmmount);
        }

        public void ReadProductsAsync()
        {
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = EventStoreConnect.CreateConnection().ReadStreamEventsForwardAsync("salesStream", nextSliceStart,
                                                              1, false)
                                                              .Result;
                nextSliceStart = (int)currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);
         

            foreach(var evt in streamEvents)
            {
                Product p =Parser.ParseJson<Product>(evt.Event);
                products.Add(p);
            }
            
           
        }


      
        
    }
}
