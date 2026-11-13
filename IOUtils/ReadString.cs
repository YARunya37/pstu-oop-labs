using System.Globalization;
using System.Numerics;

namespace IOUtils
{
    public static class ReadString
    {
        public static string ReadStringValue(string message, string errorMessage = "")
        {
            Console.Write(message);
            string value;

            do
            {
                value = Console.ReadLine();

                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Некорректный ввод! " + errorMessage);
                }
            }while(string.IsNullOrEmpty(value));

            Console.WriteLine();
            return value;
        }
        
        public static T ReadNumberValue<T>(string message, string errorMessage = "") where T:INumber<T>
        {
            Console.Write(message);
            bool isParsed = false;
            T value;

            do
            {
                isParsed = T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out value);
                if (!isParsed)
                {
                    Console.WriteLine("Некорректный ввод! " + errorMessage);
                }
            }while(!isParsed);

            Console.WriteLine();
            return value;
        }


        public static T ReadPositiveNumber<T>(string message, string errorMessage = "") where T : INumber<T>
        {
            Console.Write(message);
            bool isValid = false;
            T value;

            do
            {
                // 1. Сначала пытаемся распарсить строку в число
                bool isParsed = T.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, out value);

                // 2. Если распарсилось, проверяем, что число больше нуля
                if (isParsed && IsPositive(value))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод! Число должно быть положительным. " + errorMessage);
                    // Если ввод некорректный, выводим приглашение к вводу заново
                    Console.Write(message); 
                }
            } while (!isValid);

            Console.WriteLine();
            return value;
        }

        // Считывает ряд чисел разделенных пробелами. Возвращает массив чисел, указанного типа
        public static T[] ReadNumberSequence<T>(string message = "") where T : INumber<T>
        {
            // Если переданно сообщение, то выводим его
            if(message != "")
                Console.WriteLine(message);

            try
            {
                // Считываем последовательность ч
                T[] sequence = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => T.Parse(s, CultureInfo.CurrentCulture))
                    .ToArray();
                
                return sequence;
            }catch(Exception e)
            {
                Console.WriteLine("Ошибка ввода! " + e);
            }
            T[] arr = {};
            return arr;
        }

        static bool IsPositive<T>(T value) where T : INumber<T>
        {
            return value > T.Zero;
        }


    }
}