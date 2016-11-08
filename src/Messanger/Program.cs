using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messanger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReceiveMessage receive = new ReceiveMessage();
            //receive.Consume(args);
            SendMessage send = new SendMessage();
            //send.Publish(args);
        }
    }
}
