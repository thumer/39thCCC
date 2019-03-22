using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TCPHelper
    {
        TcpClient client;
        NetworkStream stream;

        public void Connect()
        {
            try
            {
                Int32 port = 13000;
                client = new TcpClient("localhost", port);
                stream = client.GetStream();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Connect ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Connect SocketException: {0}", e);
            }
        } // Connect

        public void Disconnect()
        {
            if (client == null)
                return;

            try
            {
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Disconnect ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Disconnect SocketException: {0}", e);
            }
        } // Connect

        public void SendMessage(String message)
        {
            if (stream == null)
                return;

            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Send ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Send SocketException: {0}", e);
            }

        }

        public String ReceiveMessage()
        {
            if (stream == null)
                return String.Empty;

            String responseData = String.Empty;

            try
            {
                Byte[] data = new Byte[512];

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Received: {0}", responseData);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Receive ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Receive SocketException: {0}", e);
            }

            return responseData;
        }


    }
}
