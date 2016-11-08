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
        string message = "Send my message";
        public SendMessage()
        {
            var factory = new ConnectionFactory { HostName = "LocalHost" };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }
        public void Publish(string input)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                routingKey: message,
                basicProperties: null,
                body: body);
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
