using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Messanger
{
    public class ReceiveMessage : IDisposable
    {
        ConnectionFactory connection;
        public ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
           channel = connection.CreateModel();

        }

        IModel channel;
        public void Consume(Action<string> action)
        {
            var queuename = "hello"; //channel.QueueDeclare().QueueName;
            channel.QueueDeclare(queue: queuename,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                action(message);
                //Console.WriteLine("Received message is {0}", s);
            };
            channel.BasicConsume(queue: queuename,
                   noAck: true,
                   consumer: consumer);
            // Console.WriteLine(" Press [enter] to exit.");
            // Console.ReadLine();
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
