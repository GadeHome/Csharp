using System;

namespace FirstApp
{
    class Program
    {
        public static int task(int[] a)
        {
            int count = 0;
            foreach (int num in a)
            {
                if (num > 0)
                    count++;
            }

            return count;
        }

        public static int task2(int n)
        {
            int b = 0;
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    b = i;
                    break;
                }
            }
            return b;
        }

        public static void test_task()
        {

            int[] a1 = { 2, -5, 7, -9 };
            int _count = task(a1);
            if (_count == 2)
                Console.WriteLine("OK");
            else
                Console.WriteLine("FAIL");
        }
        static void Main(string[] args)
        {
            int[] a = { 2, -5, 7, -9 };
            int count = task(a);
            task2(7);
            test_task();
            Console.WriteLine($"Количество положительных чисел в массиве: {count}");
        }
    }
}