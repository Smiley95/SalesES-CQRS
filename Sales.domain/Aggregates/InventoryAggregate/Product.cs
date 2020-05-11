using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Aggregates.InventoryAggregate
{
    public class Product
    {
        public readonly Guid Id;
        public string Name;
        public int SoldQuantity;
        public Product(string name, int soldQuantity)
        {
            Id = Guid.NewGuid();
            SoldQuantity= soldQuantity;
            Name = name;
        }
    }
}
