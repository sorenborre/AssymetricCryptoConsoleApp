using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AssymetricCryptoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpServer server = new TcpServer();


            RsaCspKey rcp = new RsaCspKey();
            CspParameters cp = new CspParameters(1);

            cp.KeyContainerName = "name";
            var rsa = new RSACryptoServiceProvider(2048, cp);

            server.Start(rcp.GetRsaPublicParams(rsa));

            byte[] text = Encoding.UTF8.GetBytes("asd");

            byte[] encrypted = rcp.Encrypt(text, rcp.GetRsaPublicParams(rsa));

            byte[] decrypted = rcp.Decrypt(encrypted, rcp.GetRsaPrivateParams(rsa));

            Console.WriteLine(Encoding.Default.GetString(decrypted));
        }
    }
}
