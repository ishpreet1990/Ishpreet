using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Messanger;

namespace RabbitMQTests
{
    public class Program
    {
        public static string First = "Hello";
        public static void Main(string[] args)
        {
            
        }

        [Fact]
        public void SentMessage()
        {
            ReceiveMessage receive = new ReceiveMessage();
            receive.StartListening();

            SendMessage sender = new SendMessage();
            sender.SendMyMessage(First);


            //Assert.Equal("Hello",);
        }
    }
}
