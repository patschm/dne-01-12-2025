

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
        //ViaTasks();
        //MetFouten();
        NuViaDeHippeMethode();

        Task.Delay(7000).ContinueWith(pt => Console.WriteLine("Iets anders"));
        Console.WriteLine("Einde programma");
        Console.ReadLine();
    }

    private static async Task NuViaDeHippeMethode()
    {
        int x = 3;
        var t3 = Task.Run(() => LongAdd(9, 5));
    
        int result = await t3;
        x = 4;
        Console.WriteLine($"{result} En verder");

        result = await Task.Run(() => LongAdd(9, 15));
        Console.WriteLine($"{result} En nog verder...");

        result = await LongAddAsync(10, 23);
        Console.WriteLine($"{result} En nog verder...");

        try
        {
            await Task.Run(async () =>
            {
                await Task.Delay(2000);
                throw new Exception("Ooops");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void MetFouten()
    {
        Task.Run(() =>
        {
            Task.Delay(2000).Wait();
            throw new Exception("Ooops");
        }).ContinueWith(pt =>
        {
            if (pt.Status == TaskStatus.Faulted)
            {
                Console.WriteLine(pt.Exception?.InnerException?.Message);
                return;
            }
            Console.WriteLine("Ging toch goed!");
        });

        //try
        //{
        //    Task.Run(() => {
        //        Task.Delay(2000).Wait();
        //        throw new Exception("Ooops");
        //    });
        //}
        //catch (Exception ex) 
        //{ 
        //    Console.WriteLine(ex.Message);
        //}
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

        IAsyncResult ar = del.BeginInvoke(5, 6, ar =>
        {
            int result = del.EndInvoke(ar);
            Console.WriteLine(result);
        }, null);
    }

    private static void ViaThreadPool()
    {
        ThreadPool.QueueUserWorkItem((o) => Synchroon());
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
    static Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a, b));
    }
}
