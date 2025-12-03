using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Confidentiality;

internal class Program
{
    static void Main(string[] args)
    {
        //TestSymmetrisch();
        TestAsymmetrisch();
    }


    static string publicKey;
   
    private static void TestAsymmetrisch()
    {
       var dsa =  GeneratePublicKeyForSender();
        byte[] crypto = SenderAsymmetrisch();
        Console.WriteLine(Encoding.UTF8.GetString(crypto));
        OntvangerAsymmetrisch(dsa, crypto);
    }

    private static RSA GeneratePublicKeyForSender()
    {
        RSA alg = RSA.Create();
        publicKey = alg.ToXmlString(false);
        return alg;
       
    }

    private static void OntvangerAsymmetrisch(RSA alg,  byte[] crypto)
    {
        var cip = alg.Decrypt(crypto, RSAEncryptionPadding.Pkcs1);
        Console.WriteLine(Encoding.UTF8.GetString(cip));
    }

    private static byte[] SenderAsymmetrisch()
    {
        string document = "Hello World!";
        RSA alg = RSA.Create();
        alg.FromXmlString(publicKey);
        return alg.Encrypt(Encoding.UTF8.GetBytes(document), RSAEncryptionPadding.Pkcs1);
    }

    private static void TestSymmetrisch()
    {
        byte[] crypto = SenderSymmetrisch();
        Console.WriteLine(Encoding.UTF8.GetString(crypto));
        OntvangerSymmetrisch(crypto);
    }

    static byte[] key;
    static byte[] iv;

    private static void OntvangerSymmetrisch(byte[] crypto)
    {
        Aes algorithm = Aes.Create();
        algorithm.Mode = CipherMode.CBC;
        algorithm.Key = key;
        algorithm.IV = iv;

        using (var mem = new MemoryStream(crypto))
        {
            using (var crypt = new CryptoStream(mem, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (var reader = new StreamReader(crypt))
                {
                    string document = reader.ReadToEnd();
                    Console.WriteLine(document);
                }
            }
        }
    }

    private static byte[] SenderSymmetrisch()
    {
        string document = "Hello World!";

        Aes algorithm = Aes.Create();
        algorithm.Mode = CipherMode.CBC;
        key = algorithm.Key;
        iv = algorithm.IV;

        using (var mem = new MemoryStream())
        {
            using (var crypt = new CryptoStream(mem, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var writer = new StreamWriter(crypt))
                {
                    writer.Write(document);
                }
            }

            return mem.ToArray();
        }
    }
}
