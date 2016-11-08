using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

namespace Messanger
{
    public class ReceiveMessage : IDisposable
    {
        //ConnectionFactory connection;
        public ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

        }

        IModel _channel;

        public void Consume(Action<string> action)
        {
            _channel.ExchangeDeclare(exchange: "logs",type: "fanout");

            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");
            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                action(message);
                Console.WriteLine("Received message is {0}", message);
            };
            _channel.BasicConsume(queue: "message sent",
                  noAck: true,
                  consumer: consumer);
            Console.WriteLine(" Press [enter] to see the message.");
          //  Console.ReadLine();
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
