

using System.Linq.Expressions;

namespace Draadjes;

internal class Program
{
    static void Main(string[] args)
    {
        //Synchroon();
        //ViaThread();
        //ViaThreadPool();
        // ViaDelegates();
        ViaTasks();

        Console.WriteLine("Einde programma");
       Console.ReadLine();
    }

    private static void ViaTasks()
    {

        Task t1 = new Task(() =>
        {
            var result = LongAdd(3, 4);
            Console.WriteLine($"Het resultaat is {result}");
        });

        t1.Start();


        Task t2 = Task.Run(() =>
        {
            var result = LongAdd(3, 5);
            Console.WriteLine($"Het resultaat is {result}");
        });


        var t3 = Task.Run(() => LongAdd(9, 5)).ContinueWith(previousTask =>
        {
            int result = previousTask.Result;

            Console.WriteLine($"Het resultaat is {result}");
        });
        t3.ContinueWith(pt => Console.WriteLine("Doen we ook"));
        
    }

    private static void ViaDelegates()
    {
        Func<int, int, int> del = LongAdd;

        IAsyncResult ar = del.BeginInvoke(5, 6, ar => {
            int result = del.EndInvoke(ar);
            Console.WriteLine(result);
        }, null);  
    }

    private static void ViaThreadPool()
    {
        ThreadPool.QueueUserWorkItem((o)=>Synchroon());
    }

    private static void ViaThread()
    {
       
        ThreadStart start = new ThreadStart(Synchroon);
        Thread th = new Thread(start);
        
        th.Start();
    }

    private static void Synchroon()
    {
        var result = LongAdd(3, 4);
        Console.WriteLine($"Het resultaat is {result}");
    }

    static int LongAdd(int a, int b)
    {
        Task.Delay(5000).Wait();
        return a + b;
    }
}
