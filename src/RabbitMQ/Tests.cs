using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using Xunit;
using Messanger;

namespace RabbitQTests
{
    public class Tests
    {
        public string input = "Hello";

        [Fact]
        public void SentMessage()
        {
            using (var listener = new ReceiveMessage())
            using (var wait = new ManualResetEvent(false))
            {
                string output = null;
                listener.Consume(r =>
                {
                    output = r;
                    wait.Set();
                });
                using (var sender = new SendMessage())
                {
                    sender.Publish(input);
                }
                
                Assert.Equal("Hello", output);
            }

        }
    }
}
