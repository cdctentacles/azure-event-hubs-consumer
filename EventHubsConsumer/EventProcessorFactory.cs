using System;
using System.Collections.Generic;
using System.Text;
using CDC.EventCollector;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;


namespace EventHubsConsumer
{
    class EventProcessorFactory : IEventProcessorFactory
    {
        private IEventCollector collector;

        public EventProcessorFactory(IEventCollector collector)
        {
            this.collector = collector;
        }

        IEventProcessor IEventProcessorFactory.CreateEventProcessor(PartitionContext context)
        {
            return new SimpleEventProcessor(context, collector);
        }
    }
}