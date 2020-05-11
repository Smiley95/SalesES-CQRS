using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Aggregates
{
    public class Sale
    {
        public readonly Guid Id;
        public int Quantity;
        public Product Product;
        public double Price;
        public Sale(int quantity, string productName, double price)
        {
            this.Id = Guid.NewGuid();
            this.Quantity = quantity;
            this.Price = price;
            this.Product = new Product(productName, (price / quantity));

        }
    }
}
