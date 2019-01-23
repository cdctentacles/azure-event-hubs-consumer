using System;
using System.Collections.Generic;
using System.Text;
using CDC.EventCollector;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace EventHubsConsumer
{
    public class CollectorEventsProducer : Source
    {
        private const string EventHubConnectionString = "{Event Hubs connection string}";
        private const string EventHubName = "{Event Hub path/name}";
        private const string StorageContainerName = "{Storage account container name}";
        private const string StorageAccountName = "{Storage account name}";
        private const string StorageAccountKey = "{Storage account key}";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);
        public CollectorEventsProducer(IEventCollector collector, IHealthStore healthStore) :
        base(collector, healthStore)
        {
            this.Id = new Guid();
            this.collector = collector;
        }

        public override Guid GetSourceId()
        {
            return this.Id;
        }

        public override ITransactionalLog GetTransactionalLog()
        {
            return null;
        }

        public IEventCollector Collector
        {
            get { return this.collector; }
        }

        public async void StartEventProcessor()
        {
            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            this.eventProcessorHost = eventProcessorHost;

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();
        }

        public async void CloseEventProcessor()
        {
            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }

        Guid Id;
        private IEventCollector collector;
        private EventProcessorHost eventProcessorHost;
    }
}
