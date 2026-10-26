using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0.1, b = 0.8;
            const double e = 0.0001;
            int n = 35;

            Console.WriteLine("Вычисление функций с использованием их разложения в   степенной ряд\n");
            // Количество шагов
            int k = 10;

            double step = (b-a)/k;
            
            for (double x = a; x <= b + step/2; x+= step)
            {
                // Вычисление суммы ряда для заданного количества членов n

                double SN = 0;

                double lastPower = 1;
                for (int _i = 1; _i <= n; _i++)
                {
                    lastPower *= x;
                    double _ai = lastPower * Math.Cos(_i * Math.PI / 3) / _i;
                    SN += _ai;
                }   


                // Вычисление значения по степенному ряду с заданной точностью

                double SE = 0;
                int i = 1;


                lastPower = 1;
                double ai = x * Math.Cos(i * Math.PI / 3) / i;
                
                while (Math.Abs(ai) >= e)
                {
                    lastPower*=x;
                    ai = lastPower * Math.Cos(i * Math.PI / 3) / i;

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