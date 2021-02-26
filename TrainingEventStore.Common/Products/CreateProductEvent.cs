using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingEventStore.Common.Products
{
    public class CreateProductEvent :Event
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public CreateProductEvent(Guid productId, string productName, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }


    }
}
