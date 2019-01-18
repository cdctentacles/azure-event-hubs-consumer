using System;
using System.Collections.Generic;
using System.Text;
using CDC.EventCollector;

namespace EventHubsConsumer
{
    class CollectorEventsProducer
    {
        IEventCollector CDCCollector;

        public CollectorEventsProducer()
        {
            var sourceFactories = new List<ISourceFactory>();
            var persistentCollectors = new List<IPersistentCollector>();
            var conf = new Configuration(sourceFactories, persistentCollectors)
                    .SetHealthStore(new TestHealthStore());
            CDCCollector.AddConfiguration(conf);
        }
    }
}
