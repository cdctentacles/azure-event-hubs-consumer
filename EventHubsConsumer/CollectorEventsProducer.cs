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

        public void StartEventProcessor()
        {
            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();
        }

        Guid Id;
        private IEventCollector collector;
    }
}
