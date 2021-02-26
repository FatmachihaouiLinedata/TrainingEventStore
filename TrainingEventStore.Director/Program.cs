using System;

namespace TrainingEventStore.Director
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectorServices directorServices = new DirectorServices();
            directorServices.SubscribeToSaleEvent();
        }
    }
}
