using System;
using System.Diagnostics;

namespace TrainingEventStoreMain
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var process1 = new Process())
            {
                process1.StartInfo.FileName = @"C:\Users\FChihaoui\source\repos\TrainingEventStore\TrainingEventStore.Director\bin\Debug\netcoreapp3.1\TrainingEventStore.Director.exe";
                process1.Start();
            }

            using (var process2 = new Process())
            {
                process2.StartInfo.FileName = @"C:\Users\FChihaoui\source\repos\TrainingEventStore\TrainingEventStore.InventoryManager\bin\Debug\netcoreapp3.1\TrainingEventStore.InventoryManager.exe";
                process2.Start();
            }

            Console.WriteLine("MainApp");
            Console.ReadKey();
        }
    }
}
