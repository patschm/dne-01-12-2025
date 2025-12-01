using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StromingsLeer;

internal class Program
{
    static void Main(string[] args)
    {
        //SchrijfNaarFile();
        //LezenVanFile();
        //SchrijfNaarFileModern();
        //LezenVanFileModern();
        // SchrijfNaarFileZipped();
        //LezenVanFileZip();
        //SchrijvenNaarXml();
        SchrijvenNaarXmlHip();
    }

    private static void SchrijvenNaarXmlHip()
    {
        Person p1 = new Person { Id = 1, FirstName = "Piet", LastName = "Janssen" };
        var file = new FileInfo(@"D:\Temp\data2.xml");

        var fs = file.Create();
        XmlWriter writer = XmlWriter.Create(fs);
        writer.WriteStartElement("people");
        
        XmlSerializer ser = new XmlSerializer(typeof(Person));

        for (int i = 0; i < 10; i++)
        {
            p1.Id = i;
            ser.Serialize(writer, p1);
        }


        writer.WriteEndElement();
        writer.Close();
    }
    private static void SchrijvenNaarXml()
    {
        var file = new FileInfo(@"D:\Temp\data.xml");

        var fs = file.Create();
        XmlWriter writer = XmlWriter.Create(fs);

        writer.WriteStartElement("person");
        writer.WriteAttributeString("id", "1");
        writer.WriteStartElement("first-name");
        writer.WriteString("Jan");
        writer.WriteEndElement();
        writer.WriteStartElement("last-name");
        writer.WriteString("de Vries");
        writer.WriteEndElement();
        writer.WriteEndElement();

        writer.Close();
    }

    private static void LezenVanFile()
    {
        Directory.CreateDirectory(@"D:\Temp");
        var file = new FileInfo(@"D:\Temp\data.txt");
        FileStream fs = file.OpenRead();
        byte[] buffer = new byte[32000];

        int bytesRead = 1;
        while (bytesRead > 0)
        {
            Array.Clear(buffer, 0, buffer.Length);
            bytesRead = fs.Read(buffer, 0, buffer.Length);
            var txt = Encoding.UTF8.GetString(buffer);
            Console.Write(txt);
        }
        
    }

    private static void SchrijfNaarFile()
    {
        var txt = "Hello World ";
        Directory.CreateDirectory(@"D:\Temp");
        var file = new FileInfo(@"D:\Temp\data.txt");
        if (!file.Exists)
        {
            FileStream fs = file.Create();
            for (int i = 0; i < 1000; i++) {
               byte[] data = Encoding.UTF8.GetBytes(txt + i + "\r\n");
                fs.Write(data, 0, data.Length);
            }
            fs.Close();
        }
       
    }

    private static void SchrijfNaarFileModern()
    {
        var txt = "Hello World ";
        Directory.CreateDirectory(@"D:\Temp");
        var file = new FileInfo(@"D:\Temp\data2.txt");
        if (!file.Exists)
        {
            FileStream fs = file.Create();
            StreamWriter writer = new StreamWriter(fs);
            for (int i = 0; i < 1000; i++)
            {
                writer.WriteLine(txt + i);
            }
            writer.Close();

            //writer.Flush();
            //fs.Close();
        }

    }
    private static void LezenVanFileModern()
    {
        Directory.CreateDirectory(@"D:\Temp");
        var file = new FileInfo(@"D:\Temp\data2.txt");
        FileStream fs = file.OpenRead();
        StreamReader reader = new StreamReader(fs);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
        reader.Close(); 
    }
    private static void SchrijfNaarFileZipped()
    {
        var txt = "Hello World ";
        Directory.CreateDirectory(@"D:\Temp");
        var file = new FileInfo(@"D:\Temp\data.zip");
        if (!file.Exists)
        {
            FileStream fs = file.Create();
            GZipStream zipper = new GZipStream(fs, CompressionMode.Compress);
            StreamWriter writer = new StreamWriter(zipper);
            for (int i = 0; i < 1000; i++)
            {
                writer.WriteLine(txt + i);
            }
            writer.Close();

            //writer.Flush();
            //fs.Close();
        }

    }

    private static void LezenVanFileZip()
    {
        Directory.CreateDirectory(@"D:\Temp");
        var file = new FileInfo(@"D:\Temp\data.zip");
        FileStream fs = file.OpenRead();
        GZipStream zipper = new GZipStream(fs, CompressionMode.Decompress);
        StreamReader reader = new StreamReader(zipper);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
        reader.Close();
    }
}
