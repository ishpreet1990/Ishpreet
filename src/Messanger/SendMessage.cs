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
            var factory = new ConnectionFactory { HostName = "localHost" };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "logs", type: "fanout");
        }
        public void Publish(string input)
        {
            var message = GetMessage(input);
            var body = Encoding.UTF8.GetBytes(message);
            
            channel.BasicPublish(exchange: "logs",
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            //Console.WriteLine(" Sent {0}", message);
        }

        public static string GetMessage(string input)
        {
            return ((input.Length > 0) ? string.Join("", input) : "info: hello how are you");
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
