using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingEventStore.Common.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product()
        {

        }

    }
}
