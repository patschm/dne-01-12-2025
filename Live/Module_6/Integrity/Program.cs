
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Integrity;

internal class Program
{
    static void Main(string[] args)
    {

        DSA dsa = DSA.Create();
        Console.WriteLine(dsa.ToXmlString(false));
        Console.WriteLine(new string('=', 80));
        Console.WriteLine(dsa.ToXmlString(true));
        // TestHash();
        //TestSymmetrisch();
        TestAsymmetrisch();

    }

    private static void TestAsymmetrisch()
    {
        (byte[] Signature, string Document, string PublicKey) result = SenderAsymmetrisch();
        Console.WriteLine(Convert.ToBase64String(result.Signature));
        Console.WriteLine(result.Document);
        result.Document += "!"; // Ed in de bocht
        OntvangerAsymmetrisch(result);
    }

    private static void TestSymmetrisch()
    {
        (byte[] Hash, string Document) result = SenderSymmetrisch();
        Console.WriteLine(Convert.ToBase64String(result.Hash));
        Console.WriteLine(result.Document);
        //result.Document += "!"; // Ed in de bocht
        OntvangerSymmetrisch(result);
    }

    private static void TestHash()
    {
        (byte[] Hash, string Document) result = SenderSymmetrisch();
        Console.WriteLine(Convert.ToBase64String(result.Hash));
        Console.WriteLine(result.Document);
        result.Document += "!"; // Ed in de bocht
        OntvangerSymmetrisch(result);
        ;
    }

    private static void OntvangerAsymmetrisch((byte[] Signature, string Document, string PublicKey) result)
    {
        SHA1 sha1 = SHA1.Create();
        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(result.Document));
        DSA dsa = DSA.Create();
        dsa.FromXmlString(result.PublicKey);
        bool isOk = dsa.VerifySignature(hash, result.Signature);
        Console.WriteLine(isOk ? "Prima. Origineel":"Hier is aan gemorreld");
    }

    private static (byte[] Signature, string Document, string PublicKey) SenderAsymmetrisch()
    {
        string document = "Hello World!";

        SHA1 sha1 = SHA1.Create();
        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(document));

        DSA dsa = DSA.Create();
        string publicKey = dsa.ToXmlString(false);
        byte[] signature = dsa.CreateSignature(hash);

        return (signature, document, publicKey);
    }
    private static void OntvangerSymmetrisch((byte[] Hash, string Document) result)
    {
        HMACSHA256 hmac = new HMACSHA256();
        hmac.Key = Encoding.UTF8.GetBytes("Pa$$w0rd");

        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(result.Document));
        Console.WriteLine(Convert.ToBase64String(hash));
        Console.WriteLine(Convert.ToBase64String(result.Hash));
    }

    private static (byte[] Hash, string Document) SenderSymmetrisch()
    {
        string document = "Hello World!";

        HMACSHA256 hmac = new HMACSHA256();
        hmac.Key = Encoding.UTF8.GetBytes("Pa$$w0rd");
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(document));
        
        return (hash, document);
    }
    private static void OntvangerHash((byte[] Hash, string Document) result)
    {
        SHA1 sha1 = SHA1.Create();
        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(result.Document));
        Console.WriteLine(Convert.ToBase64String(hash));
        Console.WriteLine(Convert.ToBase64String(result.Hash));
    }

    private static (byte[] Hash, string Document) SenderHash()
    {
        string document = "Hello World!";

        SHA1 sha1 = SHA1.Create();
        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(document));

        return (hash, document);
    }
}
