namespace VuilnisMan;

internal class Program
{
    static MemDb db1 = new MemDb();
    static MemDb db2 = new MemDb();

    static void Main(string[] args)
    {

        try
        {
            db1.Open();
        }
        finally
        {
            db1.Dispose();
            db1 = null;
        }

        using (db2)
        {
            db2.Open();
        }
        db2 = null;

        using (MemDb db3 = new MemDb())
        {
            db3.Open();
        }

       // GC.Collect();
        //GC.WaitForPendingFinalizers();
        var db4 = new MemDb();
        db4.Open();
        db4 = null;

        Console.ReadLine();
    }
}
