using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace CryptoServiceReciever
{
    class Program
    {
        private const int portNum = 5999;
        private const string hostName = "host.contoso.com";

        public static int Main(String[] args)
        {
            try
            {
                var client = new TcpClient("127.0.0.1", portNum);

                NetworkStream ns = client.GetStream();

                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);

                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                client.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return 0;
        }
    }
}
