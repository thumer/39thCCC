using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class CommunicationOverTCP
    {
        class Program
        {
            static void Main(string[] args)
            {

                // Client anlegen
                TCPHelper helper = new TCPHelper();
                helper.Connect();

                int tr = 100;
                int br = 0;

                do
                {
                    Listen(helper);

                    String key = Console.ReadLine();
                    if (key.Length > 0)
                    {
                        if (key == "q")
                            break;

                        String[] tokens = key.Split(' ');
                        tr = int.Parse(tokens[0]);
                        br = int.Parse(tokens[1]);
                    }
                    String move = "throttle " + tr + "\r\nbrake " + br + "\r\n";
                    helper.SendMessage(move);

                } while (true);

                helper.Disconnect();
            }

            private static void Listen(TCPHelper helper)
            {
                String result = String.Empty;
                do
                {
                    result = helper.ReceiveMessage();
                    Thread.Sleep(30);

                } while (!result.Contains("update"));
            }
        }

    }
}
