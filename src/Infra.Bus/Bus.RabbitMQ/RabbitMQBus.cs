using Domain.Core.Bus;
using Domain.Core.Commands;
using Domain.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bus.RabbitMQ
{
    public class RabbitMQBus : IEventBus, IDisposable
    {
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        //

        private static ConnectionFactory factory;
        private static IConnection connection;
        private static IModel channel;


        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Dispose()
        {
            channel?.Dispose();
            connection?.Dispose();
        }

        public void Publish<T>(T @event) where T : Event
        {
            if (connection == null)
            {
                factory = new ConnectionFactory() { HostName = "localhost" };
                connection = factory.CreateConnection();
                factory.AutomaticRecoveryEnabled = true;
                factory.TopologyRecoveryEnabled = true;
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);
                factory.UseBackgroundThreadsForIO = true;
                factory.RequestedHeartbeat = TimeSpan.FromSeconds(3);
                channel = connection.CreateModel();
                var prop = channel.CreateBasicProperties();
                prop.DeliveryMode = 2;
            }
            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", eventName, null, body);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
