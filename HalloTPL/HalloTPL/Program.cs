using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 1000000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));

            var t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(600);
                throw new ExecutionEngineException();
                Console.WriteLine("T1 fertig");
            });

            t1.ContinueWith(t =>
            {
                Console.WriteLine("T1 Continue (kommt immer)");
            });

            t1.ContinueWith(t =>
                      {
                          Console.WriteLine("T1 OK");
                      }, TaskContinuationOptions.OnlyOnRanToCompletion);

            t1.ContinueWith(t =>
            {
                Console.WriteLine($"T1 error {t.Exception.InnerException.Message}");
            }, TaskContinuationOptions.OnlyOnFaulted);



            var t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(400);
                Console.WriteLine("T2 fertig");
                return 34567890;
            });

            t1.Start();
            t2.Start();

            //t2.Wait();
            Console.WriteLine($"Result T2: {t2.Result}");

            Task.WaitAll(t2, t1);

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
