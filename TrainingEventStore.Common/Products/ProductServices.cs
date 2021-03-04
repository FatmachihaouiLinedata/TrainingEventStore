using EventStore.ClientAPI;
using System;

namespace TrainingEventStore.Common.Products
{
    public class ProductServices
    {
        
      
        public ProductServices()
        {      
            
        }
        public void AddProduct(ProductEventCreated productEventCreated)
        {
            var evtdatatype = Parser.GetEventData(productEventCreated);

            EventStoreConnect.CreateConnection().AppendToStreamAsync("ProductStream", ExpectedVersion.Any, evtdatatype).Wait();
        }






    }
    
}
