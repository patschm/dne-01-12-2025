using System.Collections.Concurrent;

namespace ExtraOpgave;

internal class Program
{
    private static object stokje = new object();

    static void Main(string[] args)
    {
        ThreadPool.SetMaxThreads(5, 0);
        Queue<int> queue = new Queue<int>([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
        ConcurrentQueue<int> queue2 = new ConcurrentQueue<int>(queue);

        var cd = new CountdownEvent(5);
        for(int i = 0; i < 5; i++)
        {
            Task.Run(() =>
            {
                //while (queue.TryDequeue(out int val))
                do
                {
                    int val;
                    if (!queue.TryDequeue(out val)) break;
                    //lock (stokje)
                    //{
                    //    if (!queue.TryDequeue(out val))
                    //    {
                    //        break;
                    //    }
                    //}
                    Console.WriteLine($"{i} behandelt nu {val}");
                    Task.Delay(1000).Wait();
                }
                while (true);
                cd.Signal();
            });
        }

        cd.Wait();
        Console.WriteLine("De queue is nu leeg. En verder.......");
        Console.ReadLine();
    }
}
