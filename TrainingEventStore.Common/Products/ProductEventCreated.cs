using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingEventStore.Common.Products
{
    public class ProductEventCreated :Event
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public ProductEventCreated(Guid id, string productName, decimal price, int quantity)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }


    }
}
