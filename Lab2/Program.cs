using System;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1
            Console.Write("Введите число элементов: ");
            int n = int.Parse(Console.ReadLine());
            
            int countOdd = 0;
            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите число {0}: ", i + 1);
                int number = int.Parse(Console.ReadLine());

                if (number % 2 == 1)
                    countOdd++;
            }

            Console.WriteLine($"Всего нечетных чисел: {countOdd}");

            // Задание 2
            int minNum = int.MaxValue;
            int num;
            while (true)
            {
                Console.Write("Введите число (0 для выхода): ");
                num = int.Parse(Console.ReadLine());
                if (num == 0)
                    break;
                if (num < minNum)
                    minNum = num;
            }
            Console.WriteLine($"Минимальное значение: {minNum}");


            // Задание 3 
            for (int i = 0; i < 100; i++)
            {
                int currNum = Random.Shared.Next(-100, 101);

                if(currNum < 0)
                {
                    Console.WriteLine($"Номер первого отрицательного числа: {i+1}");
                    break;
                }
            }
        }
    }
}