

using System.Linq.Expressions;
using System.Threading.Tasks;

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
        //NuViaDeHippeMethode();
        // Afbreken();
        //Zaklampjes_1();
        // Zaklampjes_2();
        //Orchestration();
        AdvancedDingen();
        //Task.Delay(7000).ContinueWith(pt => Console.WriteLine("Iets anders"));
        Console.WriteLine("Einde programma");
        Console.ReadLine();
    }

    private static void AdvancedDingen()
    {
        Barrier barrier = new Barrier(10);
        Parallel.For(0, 10, i =>
        //for (int i = 0; i < 10; i++)
        {
            //Task.Run(() =>
            //{
                Console.WriteLine($"{i} gaat naar de startblokken");
                Task.Delay(Random.Shared.Next(1000, 3000)).Wait();
                Console.WriteLine($"{i} is gereed");
                barrier.SignalAndWait();
                Console.WriteLine($"{i} spuit er vandoor");
            //});
        });

        CountdownEvent cd = new CountdownEvent(10);
        Parallel.For(0, 10, i => {
            Console.WriteLine($"{i} iss bezig..");
            Task.Delay(Random.Shared.Next(1000, 3000)).Wait();
            cd.Signal();
        });


        cd.Wait();
        Console.WriteLine("En doorrrrrrrrrrrrrrrrr....");


        ThreadPool.SetMinThreads(20, 0);
        Semaphore sem = new Semaphore(5, 5);
        Parallel.For(0, 20, i =>
        {
            Console.WriteLine($"{i}wacht..");
            sem.WaitOne();
            Console.WriteLine($"{i} is binnen");
            Task.Delay(Random.Shared.Next(1000, 3000)).Wait();
            sem.Release();
            Console.WriteLine($"{i} gaat eruit");
        });




    }

    static object stokje = new object();

    private static void Orchestration()
    {
        int counter = 0;
        ThreadPool.SetMinThreads(10, 0);

        for (int i = 0; i < 10; i++)
        {
            Task.Run(() =>
            {
                //Monitor.Enter(stokje);
                lock (stokje)
                {
                    int tmp = counter;
                    Task.Delay(100).Wait();
                    tmp++;
                    counter = tmp;
                    Console.WriteLine(counter);
                }
                //Monitor.Exit(stokje);
            });
        }

        //Barrier
        //Semaphore
        //CountdownEvent

    }

    private static void Zaklampjes_2()
    {
        int a = 0;
        int b = 0;

        var azl1 = new ManualResetEvent(false);
        var azl2 = new ManualResetEvent(false);

        var zl1 = Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            a = 10;
            azl1.Set();
        });
        var zl2 = Task.Run(() =>
        {
            Task.Delay(2000).Wait();
            b = 20;
            azl2.Set();
        });

        Task.Run(() =>
        {
            //Task.WaitAll(zl1, zl2);
            Task.Delay(10).Wait();
            WaitHandle.WaitAll([azl1, azl2]);
            int c = a + b;
            Console.WriteLine($"All {c}");
        });
        Task.Run(() =>
        {
            // Task.WaitAny(zl1, zl2);
            WaitHandle.WaitAny([azl1, azl2]);
            int c = a + b;
            Console.WriteLine($"Any {c}");
        });
        //await Task.WhenAll(zl1, zl2);
        // int c = a + b;
        //Console.WriteLine(c);
    }

    private static void Zaklampjes_1()
    {
        int a = 0;
        int b = 0;

        var zl1 = new ManualResetEventSlim(false);
        var zl2 = new ManualResetEventSlim(false);

        Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            a = 10;
            zl1.Set();
        });
        Task.Run(() =>
        {
            Task.Delay(2000).Wait();
            b = 20;
            zl2.Set();
        });


        WaitHandle.WaitAny([zl1.WaitHandle, zl2.WaitHandle]);
        int c = a + b;
        Console.WriteLine(c);
    }

    private static void Afbreken()
    {
        CancellationTokenSource nikko = new CancellationTokenSource();

        CancellationToken bommetje = nikko.Token;
        Task.Run(() =>
        {
            for (int i = 0; i < 1000; i++)
            {
                if (bommetje.IsCancellationRequested)
                {
                    Console.WriteLine("Bye bye");
                    return;
                }
                Task.Delay(1000).Wait();
                Console.WriteLine($"Iteratie {i}");
            }
        });

        //Task.Delay(10000).Wait();
        nikko.CancelAfter(5000);

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
