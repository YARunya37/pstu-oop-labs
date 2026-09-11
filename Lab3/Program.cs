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

            // Количество шагов
            Console.Write("Введите количество шагов: ");
            int k = int.Parse(Console.ReadLine());

            double step = (b-a)/k;

            for (double x = a; x <= b; x+= step)
            {
                double SN = 0;

                for (int _i = 1; _i <= n; _i++)
                {
                    double ai = Math.Pow(x, _i) * Math.Cos(_i * Math.PI / 3) / _i;
                    SN += ai;
                }   

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

                double Y = -0.5 * Math.Log( 1 - 2 * x * Math.Cos(Math.PI / 3) + x * x);

                Console.WriteLine($"X: {x:F4}\tSN: {SN:F6}\tSE: {SE:F6}\tY: {Y:F6}");
            }
        }
    }
}