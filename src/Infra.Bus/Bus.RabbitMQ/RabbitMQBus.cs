using Domain.Core.Bus;
using Domain.Core.Commands;
using Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bus.RabbitMQ
{
    public class RabbitMQBus : IEventBus, IDisposable
    {
        public RabbitMQBus()
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(T @event) where T : Event
        {
            throw new NotImplementedException();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
