using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using Sales.Domain.Aggregates;

namespace Sales.Infrastructure
{
    public class SaleEventRepository
    {
        private MyEventStore _storage;
        public SaleEventRepository(string streamName)
        {
            _storage = new MyEventStore(streamName);
        }
        public void Save(Sale sale)
        {
            _storage.SaveEvents(sale, DateTime.Now,"saving Sale");
        }
        public IEnumerable<JObject> GetSales()
        {
            return _storage.ReadAllEvents();
        }
        public double GetSalesTotalPrice()
        {
            return _storage.GetTotalAmountOfSales();
        }
        public IEnumerable<JToken> GetInventory()
        {
            JArray sales = JArray.FromObject(_storage.ReadAllEvents());
            var products = sales.Select(e => new { name = e["product"]["name"], soldQuantity = e["quantity"] }).GroupBy(prod => prod.name).Select(prod => new { product = prod.Key, totalSoldAmount = prod.Sum(x => (int)x.soldQuantity) });

            return JArray.FromObject(products);
        }
    }
}
