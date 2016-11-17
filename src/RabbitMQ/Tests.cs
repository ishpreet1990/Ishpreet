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
        [Fact]
        public void SendMessageToReceiver()
        {
            var input = "hello";
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
                Assert.Equal(input, output);
            }

        }
        [Fact]
        public void MoreThanOneReceivershouldReceiveMessage()
        {
            var input = "hello";
            var listofReceiver = new List<string>();
            var listofReceiver2 = new List<string>();
            using (var listener1 = new ReceiveMessage())
            using (var listener2 = new ReceiveMessage())
            using (var wait = new CountdownEvent(2))
            {
                listofReceiver = null;
                listener1.Consume(r =>
                {
                    listofReceiver.Add(r);
                    wait.Signal();
                });

                listener2.Consume(r =>
                {
                    listofReceiver2.Add(r);
                    wait.Signal();
                });
                
                using (var sender = new SendMessage())
                {
                    sender.Publish(input);
                }
                //Assert.True(wait.Wait(200));
                Assert.Equal(new[] { input }, listofReceiver);
             //   Assert.Equal(new[] { input }, listofReceiver2);
            }
        }
    }
}
