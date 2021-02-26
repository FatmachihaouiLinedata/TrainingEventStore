using System;
using System.Collections.Generic;
using System.Text;
using TrainingEventStore.Common;

namespace TrainingEventStore.InventoryManager
{
    public class SaleAchievedEvent : Event
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public long ProductNumber { get; set; }

        public SaleAchievedEvent(Guid id, string productName, decimal price, int quantity, long productNumber)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            ProductNumber = productNumber;
        }
    }
}
