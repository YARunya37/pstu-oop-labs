using System;

namespace Lab3
{
    class Program
    {
        static int GetInt(string message)
        {
            Console.Write(message);

            bool isParsed = false;
            int value = 0;

            do
            {
                isParsed = int.TryParse(Console.ReadLine(), out value);

                if (!isParsed)
                {
                    Console.WriteLine("Некорректный ввод! Ожидаются целые числа");
                }
            }while(!isParsed);

            Console.WriteLine();

            return value;
        }

        static bool IsPositive(int value)
        {
            return value > 0;
        }

        static void Main(string[] args)
        {
            double a = 0.1, b = 0.8;
            const double e = 0.0001;
            int n = 35;

            Console.WriteLine("Вычисление функций с использованием их разложения в   степенной ряд\n");

            // Количество шагов
            int k = GetInt($"Введите количество шагов, на которые разобъем отрезок [{a}, {b}]: ");
            while (!IsPositive(k))
            {
                Console.WriteLine("Ошибка ввода! Количество шагов должно быть больше нуля!");
                k = GetInt($"Введите количество шагов, на которые разобъем отрезок [{a}, {b}]: ");
            }


            double step = (b-a)/k;

            for (double x = a; x <= b + step/2; x+= step)
            {
                // Вычисление суммы ряда для заданного количества членов n

                double SN = 0;

                for (int _i = 1; _i <= n; _i++)
                {
                    double ai = Math.Pow(x, _i) * Math.Cos(_i * Math.PI / 3) / _i;
                    SN += ai;
                }   


                // Вычисление значения по степенному ряду с заданной точностью

                double SE = 0;
                int i = 1;

                while (true)
                {
                    double ai = Math.Pow(x, i) * Math.Cos(i * Math.PI / 3) / i;

                    if (Math.Abs(ai) < e)
                        break;

                    SE += ai;
                    i++;
                }

                // Вычисление точного значения функции
                double Y = -0.5 * Math.Log( 1 - 2 * x * Math.Cos(Math.PI / 3) + x * x);

                Console.WriteLine($"X: {x:F4}\tSN: {SN:F6}\tSE: {SE:F6}\tY: {Y:F6}");
            }
        }
    }
}