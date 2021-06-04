using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Homework_16_2
{
    class Program
    {
        static void Main()
        {
            int bottom = 0;
            int top = Int32.MaxValue;

            int parallelSum = 0;
            var watchForParallel = Stopwatch.StartNew();
            Parallel.For(bottom, top, i =>
            {
                if (Calculate(i))
                {
                    Interlocked.Increment(ref parallelSum);
                }
            });
            watchForParallel.Stop();
            Console.WriteLine($"Асинхронно | Всего подходящих чисел:{parallelSum} Затраченное время: {watchForParallel.ElapsedMilliseconds} мс.");

            int sum = 0;
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < top; i++)
            {
                if (Calculate(i))
                {
                    sum++;
                }
            }
            watch.Stop();
            Console.WriteLine($"Синхронно  | Всего подходящих чисел:{sum} Затраченное время: {watch.ElapsedMilliseconds} мс.");

            Console.ReadKey();
        }

        static bool Calculate(int i)
        {
            if (i % 10 == 0)
            {
                return true;
            }

            int n = i;
            long sumOfDigits = 0;

            do
            {
                sumOfDigits += (n % 10);
                n /= 10;
            } while (n >= 10);

            sumOfDigits += (n % 10);

            if (sumOfDigits % (i % 10) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
