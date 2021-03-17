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
   
        public int Quantity { get; set; }

      
  
        public SaleAchievedEvent(Guid id, string productName,int quantity)

        { 
            Id = id;
            ProductName = productName;
            Quantity = quantity;
        }
    }
}
