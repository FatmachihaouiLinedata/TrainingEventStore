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

        ProductServices productServices = new ProductServices();

        public SaleServices()
        {
            
        }
        public void AppendSales(List<Event> events)
        {
            foreach (var evt in events)
            {
                var evtdatatype = Parser.GetEventData(evt);
                EventStoreConnect.CreateConnection().AppendToStreamAsync("SalesStream", ExpectedVersion.Any, evtdatatype).Wait();

            }

        }
        public void AddSales()
        {
            Sale sale = new Sale();
            List<Event> sales = new List<Event>();
            do
            {
                Console.WriteLine("type the Product number to add sales");
                string productNumber = Console.ReadLine();
                sale.ProductNumber = Convert.ToInt64(productNumber);
                Console.WriteLine("quantity");
                string qte = Console.ReadLine();
                sale.Quantity = Convert.ToInt32(qte);
                sale.Id = Guid.NewGuid();
                Product product = productServices.getProductbyNumber(sale.ProductNumber);
                SaleAchievedEvent saleAchieved = new SaleAchievedEvent(sale.Id, sale.ProductName, product.Price * sale.Quantity, sale.Quantity, sale.ProductNumber);
                sales.Add(saleAchieved);
                Console.WriteLine("press q to quit or press enter to continue adding sales ");
            } while (!Console.ReadKey().KeyChar.Equals('q'));
            AppendSales(sales);
        }

        public async void ReadSales()
        {
            var readEvents = new List<ResolvedEvent>();
            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;

            do
            {
                currentSlice = await EventStoreConnect.CreateConnection().ReadStreamEventsForwardAsync(
                    "SalesStream",
                    nextSliceStart,
                    5, false

                );
                nextSliceStart = (int)currentSlice.NextEventNumber;

                readEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);

            foreach (var evt in readEvents)
            {// i didn't call parser because here i need to parse a resolvedEvent not a recorded one
                var data = Encoding.UTF8.GetString(evt.Event.Data);
                var sale = JsonConvert.DeserializeObject<Sale>(data);
              

            }
            Console.WriteLine(readEvents);

        }

    }
}
