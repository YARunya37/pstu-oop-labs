using System;

namespace LabWork
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

        static double GetDouble(string message)
        {
            Console.Write(message);

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

            Console.WriteLine();

            return value;
        }

        static void ComputeExpressions(int n, int m)
        {
            int result1 = n++*m;
            Console.Write($"m={m} n={n} n++*m = {result1}\t");
            bool result2 = m--<n;
            Console.Write($"m={m} n={n} m--<n = {result2}\t");
            bool result3 = ++m>n;
            Console.WriteLine($"m={m} n={n} ++m>n = {result3}");
            
        }

        static double CalculateFunction(double x)
        {
            double expression = x + Math.Pow(Math.Abs(x), 1.0/4); 

            if(expression < 0)
            {
                Console.WriteLine("Нельзя посчитать значение корня от отрицательного выражения!");
                return double.NaN;
            }

            return Math.Sqrt(expression) + Math.Abs(x);
        }

        static void CompareDataTypes(){
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

        static void Main(string[] args)
        {
                
            // Задание 1
            Console.WriteLine(" Задание 1. Вычисление выражений");

            int n = GetInt("n?");

            int m = GetInt("m?");

            ComputeExpressions(n, m);

            double x = GetDouble("x?");
            
            double result4 = CalculateFunction(x);

            // Результат будет равен NaN в случае если мы попытаемся взять отрицательный корень
            if(!double.IsNaN(result4))
                Console.WriteLine(result4);
            
            
            // Задание 2
            Console.WriteLine("\n Задание 2. Принадлежность точки заштрихованной области");
            double x1 = GetDouble("Введите координату x: ");
            double y1 = GetDouble("Введите координату y: ");
            
            // Точка лежит в пределах единичной окружности (x² + y² <= 1) 
            // И строго во II или IV четверти (где x и y имеют разные знаки, т.е. их произведение x * y < 0)
            bool isValid = (x1 * x1 + y1 * y1 <= 1.0) && (x1 * y1 <= 0);
            Console.WriteLine($"Точка принадлежит заштрихованной области: {isValid}");

            // Задание 3
            Console.WriteLine("\n Задание 3. Сравнение double и float");
            
            CompareDataTypes();
        }
    }
}