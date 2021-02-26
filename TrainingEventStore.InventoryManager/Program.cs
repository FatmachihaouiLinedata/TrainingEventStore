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
            productServices.AddProducts();
         //   productServices.DisplayProducts();
            Console.WriteLine("****************************");
            saleServices.AddSales();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
