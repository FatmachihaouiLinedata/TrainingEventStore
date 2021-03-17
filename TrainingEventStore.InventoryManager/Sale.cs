using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingEventStore.InventoryManager
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public long ProductNumber { get; set; }
    }
}
