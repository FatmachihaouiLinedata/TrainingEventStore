using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;

namespace TrainingEventStore.Common.Products
{
    public class ProductServices
    {
        public List<Event> Events = new List<Event>();
        private Guid Id = Guid.NewGuid();
        public ProductServices()
        {

            Events.Add(new CreateProductEvent(Id, "pc", 1200, 10));
            Events.Add(new CreateProductEvent(Id, "casque", 100, 12));
            Events.Add(new CreateProductEvent(Id, "souris", 200, 20));
            Events.Add(new CreateProductEvent(Id, "clavier", 500, 33));
            Events.Add(new CreateProductEvent(Id, "ecran", 1400, 50));
            Events.Add(new CreateProductEvent(Id, "cable", 1400, 50));

        }
        public void AddProducts()
        {
            foreach (var evt in Events)
            {
                var evtdatatype = Parser.GetEventData(evt);
                EventStoreConnect.CreateConnection().AppendToStreamAsync("ProductStream", ExpectedVersion.Any, evtdatatype).Wait();
            }
        }

        public async void DisplayProducts()
        {
            var readEvents = new List<ResolvedEvent>();
            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;

            do
            {
                currentSlice = await EventStoreConnect.CreateConnection().ReadStreamEventsForwardAsync(
                    "ProductStream",
                    nextSliceStart,
                    10, false

                );
                nextSliceStart = (int)currentSlice.NextEventNumber;

                readEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            foreach (var evt in readEvents)
            {// i didn't call parser because here i need to parse a resolvedEvent not a recorded one
                var data = Encoding.UTF8.GetString(evt.Event.Data);
                var product = JsonConvert.DeserializeObject<Product>(data);
                Console.WriteLine("product number" + evt.Event.EventNumber + "/" + "product :" + product.ProductName + "price :" + product.Price);

            }
        }
        public Product getProductbyNumber(long productNumber)
        {
            var evt = EventStoreConnect.CreateConnection().ReadEventAsync("ProductStream", productNumber, true,
                new UserCredentials("admin", "changeit")).Result;
            var data = Encoding.UTF8.GetString(evt.Event.Value.Event.Data);
            var product = JsonConvert.DeserializeObject<Product>(data);
            return product;

        }
    }
    
}
