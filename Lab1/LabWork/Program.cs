using System;

namespace LabWork
{
    class Program
    {

        static void Main(string[] args)
        {

            // Задание 1
            System.Console.WriteLine(" Задание 1");
            int n = 5;
            int m = 3;
            ComputeExpressions(n, m);

            // Задание 2
            System.Console.WriteLine("\n Задание 2");
            double x1 = double.Parse(Console.ReadLine());
            double y1 = double.Parse(Console.ReadLine());
            
            // Точка лежит в пределах единичной окружности (x² + y² <= 1) 
            // И строго во II или IV четверти (где x и y имеют разные знаки, т.е. их произведение x * y < 0)
            bool isValid = (x1 * x1 + y1 * y1 <= 1.0) && (x1 * y1 < 0);
            Console.WriteLine($"Точка принадлежит заштрихованной области: {isValid}");

            // Задание 3
            System.Console.WriteLine("\n Задание 3");
            double a = 1000;
            double b = 0.0001;
            float aFloat = 1000f;
            float bFloat = 0.0001f;
            
            Console.WriteLine("Double: {0} \t Float: {1} \t Должно быть: 1", 
                (Math.Pow((a+b), 4) - Math.Pow(a, 4))/
                (6*a*a*b*b + 4*a*Math.Pow(b, 3) + Math.Pow(b, 4) + 4*Math.Pow(a, 3)* b),

                (MathF.Pow((aFloat+bFloat), 4) - MathF.Pow(aFloat, 4))/
                (6*aFloat*aFloat*bFloat*bFloat + 4*aFloat*MathF.Pow(bFloat, 3) + MathF.Pow(bFloat, 4) + 4*MathF.Pow(aFloat, 3)* bFloat)
                );
        }

        static void ComputeExpressions(int n, int m)
        {
            try
            {
                int result1 = n++*m;
                bool result2 = m--<n;
                bool result3 = ++m>n;
                Console.WriteLine($"n++*m = {result1}");
                Console.WriteLine($"m--<n = {result2}");
                Console.WriteLine($"++m>n = {result3}");

                int x = 0;
                if (int.TryParse(Console.ReadLine(), out x))
                {
                    double result4 = CalculateFunction(x);
                    if(!double.IsNaN(result4))
                        Console.WriteLine(result4);
                    else
                        Console.WriteLine("Нельзя посчитать значение корня от отрицательного выражения!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static double CalculateFunction(double x)
        {
            return Math.Sqrt(x + Math.Pow(Math.Abs(x), 1.0/4)) + x;
        }
    }
}