using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;

namespace Messanger
{
    public class SendMessage : IDisposable
    {
        IModel channel;
        //string message = "Send my message";
        public SendMessage()
        {
            var factory = new ConnectionFactory { HostName = "LocalHost" };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "message sent",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }
        public void Publish(string[] args)
        {
            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange: "",
                routingKey: "message sent",
                basicProperties: properties,
                body: body);
            Console.WriteLine(" Sent {0}", message);
        }

        public static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join("", args) : "hello");
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
