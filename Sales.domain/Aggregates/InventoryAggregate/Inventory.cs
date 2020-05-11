using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Aggregates.InventoryAggregate
{
    public class Inventory
    {
        public readonly Guid Id;
        public List<Product> items;
        public Inventory(List<Product> products)
        {
            items = products;
        }
    }
}
