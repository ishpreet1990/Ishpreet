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
            channel = connection.CreateModel();

        }

        IModel channel;
        public void Consume(Action<string> action)
        {
            channel.QueueDeclare(queue: "message sent",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                action(message);
                Console.WriteLine("Received message is {0}", message);
                int dots = message.Split('.').Length - 1;
                Thread.Sleep(dots * 1000);
                Console.WriteLine("Done");
                Console.WriteLine(" Press [enter] to exit.");
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
             channel.BasicConsume(queue: "message sent",
                   noAck: true,
                   consumer: consumer);
            Console.ReadLine();
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
