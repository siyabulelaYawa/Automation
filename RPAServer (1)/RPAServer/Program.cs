using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CoreServer
{
    class Program
    {

        static void Main(string[] args)
        {

            Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                //Server myserver = new Server("127.0.0.1", 13000);
                Server myServer = new Server("1", 2);
            });
            t.Start();

            Console.WriteLine("Server Started...!");
            //test();
        }
        private static void test()
        {


            var Server = new UdpClient(8888);
            var ResponseData = Encoding.ASCII.GetBytes("SomeResponseData");

            while (true)
            {
                var ClientEp = new IPEndPoint(IPAddress.Any, 0);
                var ClientRequestData = Server.Receive(ref ClientEp);
                var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);

                Console.WriteLine("Recived {0} from {1}, sending response", ClientRequest, ClientEp.Address.ToString());
                Server.Send(ResponseData, ResponseData.Length, ClientEp);
            }
        }
    }
    
}
