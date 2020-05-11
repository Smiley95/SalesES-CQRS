using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EventStore.ClientAPI;
using Newtonsoft.Json.Linq;
using Sales.Domain.Aggregates;

namespace Sales.Infrastructure
{
    public class MyEventStore
    {
        private string _streamName;
        public MyEventStore(string stream)
        {
            _streamName = stream;
        }
        public IEventStoreConnection ConnectToEventStore()
        {
            var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@127.0.0.1:1113"));
            conn.ConnectAsync().Wait();
            return conn;
        }
        public void SaveEvents(Sale sale, DateTime date, string eventType)
        {
            var data = "{\"id\": \"" + sale.Id + "\"," +
                    "\"quantity\":" + sale.Quantity + "," +
                    "\"product\": {" +
                        "\"id\": \"" + sale.Product.Id + "\"," +
                        "\"name\": \"" + sale.Product.Name + "\"," +
                        "\"price\":" + sale.Product.Price + "}," +
                    "\"price\":" + sale.Price +
                "}";
            var metadata = "{" + date + "}";
            var eventPayload = new EventData(Guid.NewGuid(), eventType, true, Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(metadata));
            ConnectToEventStore().AppendToStreamAsync(_streamName, ExpectedVersion.Any, eventPayload).Wait();
        }
        public IEnumerable<JObject> ReadAllEvents()
        {
            var readEvents = ConnectToEventStore().ReadStreamEventsForwardAsync(_streamName, StreamPosition.Start, 200, true);
            List<JObject> sales = new List<JObject>();
            foreach (var evt in readEvents.Result.Events)
            {
                JObject sale = JObject.Parse(Encoding.UTF8.GetString(evt.Event.Data));
                sales.Add(sale);
            }
            return sales;
        }
        public double GetTotalAmountOfSales()
        {
            List<JObject> sales = ReadAllEvents().ToList();
            double totalAmount = 0;
            foreach (var sale in sales)
            {
                totalAmount += (double)sale["price"];
            }
            return totalAmount;
        }
        public JObject ReadLastAddedEvents()
        {
            var readEvents = ConnectToEventStore().ReadEventAsync(_streamName,0,true).Result;
            JObject sale = JObject.Parse(Encoding.UTF8.GetString(readEvents.Event.Value.Event.Data));
            return sale;
        }
    }
}
