using System;
using TrainingEventStore.Common.Products;

namespace TrainingEventStore.InventoryManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SaleServices saleServices = new SaleServices();
            ProductServices productServices = new ProductServices();

            ProductEventCreated productEventCreated = new ProductEventCreated(Guid.NewGuid(), "pc", 1200, 100);
            ProductEventCreated productEventCreated1 = new ProductEventCreated(Guid.NewGuid(), "casque", 100, 120);
            ProductEventCreated productEventCreated2 = new ProductEventCreated(Guid.NewGuid(), "souris", 200, 200);
            ProductEventCreated productEventCreated3 = new ProductEventCreated(Guid.NewGuid(), "clavier", 500, 330);
            ProductEventCreated productEventCreated4 =  new ProductEventCreated(Guid.NewGuid(), "ecran", 1400, 500);

            productServices.AddProduct(productEventCreated);
            productServices.AddProduct(productEventCreated1);
            productServices.AddProduct(productEventCreated2);
            productServices.AddProduct(productEventCreated3);
            productServices.AddProduct(productEventCreated4);

            saleServices.SubscribeToProductEvent();
            saleServices.AddSale();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
