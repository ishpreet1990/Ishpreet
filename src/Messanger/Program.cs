using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messanger
{
    public class Program
    {
        ReceiveMessage receive = new ReceiveMessage();
        public static void Main(string[] args)
        {
            var receiver1 = new ReceiveMessage();
            receiver1.Consume(Console.WriteLine);
            var receiver2 = new ReceiveMessage();
            receiver2.Consume(Console.WriteLine);

            SendMessage send = new SendMessage();
            send.Publish("");
        }
    }
}
