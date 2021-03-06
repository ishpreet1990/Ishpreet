﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using RabbitMQ.Client;

namespace SendMessage
{
    public class Send
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "LocalHost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "My Message",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    string message = "Send my message";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                        routingKey: "My Message",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine("{0}",message);
                }
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
