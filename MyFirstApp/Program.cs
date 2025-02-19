using System;

namespace FirstApp
{
    class Program
    {
        public static int task4(int[] a)
        {
            int count = 0;
            foreach (int num in a)
            {
                if (num > 0)
                    count++;
            }

            return count;
        }

        public static int task1()
        {
            int cnt = 1;
            int max_cnt = 0;
            int last_num = 0;


            while (true)
            {
                int num = int.Parse(Console.ReadLine());

                if (num == 0) break;

                if (num == last_num) cnt++;
                else if (num != last_num) cnt = 1;

                if (cnt > max_cnt) max_cnt = cnt;
            }

            return max_cnt;
        }

        public static int task2(int n)
        {
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    return i;
                }
            }
            return n;
        }

        public static void test_task5()
        {

            int[] a1 = { 2, -5, 7, -9 };
            int _count = task4(a1);
            if (_count == 2)
                Console.WriteLine("OK");
            else
                Console.WriteLine("FAIL");
        }

        static void Main(string[] args)
        {
            int[] a = { 2, -5, 7, -9 };
            int count = task4(a);
            int b = task2(7);
            test_task5();
            int cnt = task1();
            Console.WriteLine($"Максимальное число подряд идущих положительных чисел: {cnt}");
            Console.WriteLine($"Наименьший делитель числа: {b}");
            Console.WriteLine($"Количество положительных чисел в массиве: {count}");
        }
    }
}