using System.Globalization;
using System.Numerics;

namespace UserInput;

public class ReadInput
{
    public static T ReadValue<T>(string message, string errorMessage = "") where T:INumber<T>
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


    public static T ReadPositiveValue<T>(string message, string errorMessage = "") where T : INumber<T>
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

    static bool IsPositive<T>(T value) where T : INumber<T>
    {
        return value > T.Zero;
    }


}
