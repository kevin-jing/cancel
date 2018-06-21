using System;
using System.Threading.Tasks;

namespace cancel
{
    class ThreadData
    {
        public int t;
    }
    class Program
    {
        private const int _threadcount = 2;
        static bool exit = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task.Run(async () =>
            {
                Task[] taskArray = new Task[_threadcount];
                for (int t = 0; t < taskArray.Length; t++)
                {
                    taskArray[t] = await Task.Factory.StartNew(async (Object obj) =>
                    {
                        var data = obj as ThreadData;
                        Random randomNums = new Random();
                        while (true)
                        {
                            if (exit)
                            {
                                Console.WriteLine($"Thread  { data.t} exit!");
                                break;
                            }
                            await Task.Delay(randomNums.Next(1000, 2000));
                            Console.WriteLine("Thread " + data.t);
                        }
                        
                    },
                    new ThreadData() { t = t });
                }
            });

            Task.Run(async () =>
            {
                Random randomNums = new Random();
                await Task.Delay(randomNums.Next(2000, 4000));
                exit = true;
                Console.WriteLine("exit!");

            });

            Console.ReadLine();
        }
    }
}
