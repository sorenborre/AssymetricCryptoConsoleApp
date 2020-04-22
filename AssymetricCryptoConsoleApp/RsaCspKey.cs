using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AssymetricCryptoConsoleApp
{
    class RsaCspKey
    {

        public byte[] Encrypt(byte[] dataToEncrypt, RSAParameters rSAParameters)
        {
            byte[] cipherData;

            var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportParameters(rSAParameters);

            using (rsa)
            {
                cipherData = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipherData;
        }

        public byte[] Decrypt(byte[] dataToDecrypt, RSAParameters rSAParameters)
        {
            byte[] originalData;
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportParameters(rSAParameters);

            using (rsa)
            {
                originalData = rsa.Decrypt(dataToDecrypt, true);
            }
            return originalData;
        }


        public RSAParameters GetRsaPublicParams(RSACryptoServiceProvider rsa)
        {
            return rsa.ExportParameters(false);
        }
       
        public RSAParameters GetRsaPrivateParams(RSACryptoServiceProvider rsa)
        {
            return rsa.ExportParameters(true);
        }


        public void GetOrSetKeyInContainer(string ContainerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            Console.WriteLine("Key added to container: \n  {0}", rsa.ToXmlString(true));
        }

        public void DeleteKeyFromContainer(string ContainerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            rsa.PersistKeyInCsp = false;

            rsa.Clear();

            Console.WriteLine("Key deleted.");
        }
    }
}
