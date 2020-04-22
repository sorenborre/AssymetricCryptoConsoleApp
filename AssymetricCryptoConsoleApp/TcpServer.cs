using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace AssymetricCryptoConsoleApp
{
    class TcpServer
    {


        private const int portNum = 5999;

        public int Start(RSAParameters rSAParameters)
        {
            bool running = true;

            var listener = new TcpListener(IPAddress.Any, portNum);
            byte[] rsaParam = ObjectToByteArray(rSAParameters);
            listener.Start();

            while (true)
            {
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();

                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                try
                {
                    ns.Write(rsaParam, 0, rsaParam.Length);
                    ns.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            listener.Stop();

            return 0;

        }
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

    }
}
