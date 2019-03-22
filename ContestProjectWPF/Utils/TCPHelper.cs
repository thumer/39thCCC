using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContestProject.Utils
{
    public class TCPHelper
    {
        private bool _isConnected;
        private TcpClient _client;
        private NetworkStream _stream;

        public TCPHelper()
        {
        }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        public void Connect(string host, int port)
        {
            try
            {
                _client = new TcpClient(host, port);
                _stream = _client.GetStream();
                _isConnected = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}: {1}", e.GetType().Name, e.Message));
            }
        }

        public void Disconnect()
        {
            if (_client == null)
                return;

            try
            {
                _stream.Close();
                _client.Close();
                _isConnected = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}: {1}", e.GetType().Name, e.Message));
            }
        }

        public Task SendMessageAsync(string message)
        {
            return Task.Run(() => SendMessage(message));
        }

        public void SendMessage(string message)
        {
            if (_stream == null)
                return;

            message = message.Trim() + "\r\n";

            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                _stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}: {1}", e.GetType().Name, e.Message));
            }
        }

        public Task<string> ReceiveMessageAsync()
        {
            return Task.Run(() => ReceiveMessage());
        }

        public string ReceiveMessage()
        {
            if (_stream == null)
                return string.Empty;

            string responseData = string.Empty;

            try
            {
                Byte[] data = new Byte[512];

                Int32 bytes = _stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Received: {0}", responseData);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}: {1}", e.GetType().Name, e.Message));
            }

            return responseData;
        }
    }
}