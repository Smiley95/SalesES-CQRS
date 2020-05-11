using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Aggregates
{
    public class Product
    {
        public readonly Guid Id;
        public string Name;
        public double Price;
        public Product(string name, double price)
        {
            this.Id = Guid.NewGuid();
            this.Price = price;
            this.Name = name;
        }
    }
}
