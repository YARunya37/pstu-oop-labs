using System;

namespace LabWork
{
    class Program
    {

        static int GetInt()
        {
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

            return value;
        }

        static double GetDouble()
        {
            bool isParsed = false;
            double value = 0;

            do
            {
                isParsed = double.TryParse(Console.ReadLine(), out value);

                if (!isParsed)
                {
                    Console.WriteLine("Некорректный ввод! Ожидаются рациональные числа");
                }
            } while (!isParsed);

            return value;
        }

        static void Main(string[] args)
        {

            // Задание 1
            System.Console.WriteLine(" Задание 1");
            System.Console.Write("n?");
            int n = GetInt();
            System.Console.WriteLine();
            System.Console.Write("m?");
            int m = GetInt();
            System.Console.WriteLine();

            ComputeExpressions(n, m);

            System.Console.Write("x?");
            int x = GetInt();
            
            double result4 = CalculateFunction(x);

            // Результат будет равен NaN в случае если мы попытаемся взять отрицательный корень
            if(!double.IsNaN(result4))
                Console.WriteLine(result4);
            else
                Console.WriteLine("Нельзя посчитать значение корня от отрицательного выражения!");
            
            // Задание 2
            System.Console.WriteLine("\n Задание 2");
            double x1 = GetDouble();
            double y1 = GetDouble();
            
            // Точка лежит в пределах единичной окружности (x² + y² <= 1) 
            // И строго во II или IV четверти (где x и y имеют разные знаки, т.е. их произведение x * y < 0)
            bool isValid = (x1 * x1 + y1 * y1 <= 1.0) && (x1 * y1 <= 0);
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
            int result1 = n++*m;
            bool result2 = m--<n;
            bool result3 = ++m>n;
            Console.Write($"n++*m = {result1}\t");
            Console.Write($"m--<n = {result2}\t");
            Console.WriteLine($"++m>n = {result3}");
            
        }

        static double CalculateFunction(double x)
        {
            return Math.Sqrt(x + Math.Pow(Math.Abs(x), 1.0/4)) + x;
        }
    }
}