
namespace VuilnisMan;

internal class MemDb : IDisposable
{
    private static bool isOpen = false;
    private FileStream _file;

    public void Open()
    {
        Console.WriteLine("Opening.....");
        if (!isOpen)
        {
            isOpen = true;
            _file = new FileStream("x.txt", FileMode.OpenOrCreate);
            Console.WriteLine("Open");
        }
        else
        {
            Console.WriteLine("Helaas! In gebruik");
        }
    }
    public void Close()
    {
        Console.WriteLine("Closing.....");
        isOpen = false;
        Console.WriteLine("Closed");
    }

    protected void RuimOp(bool fromDispose)
    {
        Close();
        // en nog vele andere dingen
        if (fromDispose)
        {
            _file.Dispose();
        }
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        RuimOp(true);
    }

    ~MemDb()
    {
        RuimOp(false);
    }
}
