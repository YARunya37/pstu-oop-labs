
using System;
using System.Linq;
using System.Text.RegularExpressions;
using IOUtils;

public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n========= ГЛАВНОЕ МЕНЮ =========");
            Console.WriteLine("1. Двумерный массив");
            Console.WriteLine("2. Рваный массив");
            Console.WriteLine("3. Обработка строки");
            Console.WriteLine("0. Выход");

            int choice = ReadString.ReadValue<int>("Ваш выбор: ");

            switch (choice)
            {
                case 1:
                    HandleMatrix();
                    break;
                case 2:
                    HandleJaggedArray();
                    break;
                case 3:
                    HandleString();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }

    // =========================================
    // ДВУМЕРНЫЙ МАССИВ
    // =========================================

    private static void HandleMatrix()
    {
        int rows = ReadString.ReadPositiveValue<int>("Количество строк: ");

        int columns = ReadString.ReadPositiveValue<int>("Количество столбцов: ");

        int[,] matrix = new int[rows, columns];

        FillMatrix(matrix);

        Console.WriteLine("\nИсходный массив:");
        ArrayIO.PrintArray(matrix);

        int k = ReadString.ReadValue<int>("Номер столбца для удаления (нумерация с 1): ");

        try
        {
            matrix = RemoveColumn(matrix, k);

            Console.WriteLine("\nМассив после удаления столбца:");
            ArrayIO.PrintArray(matrix);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void FillMatrix<T>(T[,] matrix)
        where T : System.Numerics.INumber<T>
    {
        Console.WriteLine("1. Заполнить вручную");
        Console.WriteLine("2. Заполнить случайными числами");

        int choice = ReadString.ReadValue<int>("Выбор: ");

        switch (choice)
        {
            case 1:
                ArrayIO.FillArrayWithInput(matrix);
                break;
            case 2:
                ArrayIO.FillArrayWithRandom(matrix);
                break;
            default:
                Console.WriteLine(
                    "Неверный выбор. Выполняется ручное заполнение.");
                ArrayIO.FillArrayWithInput(matrix);
                break;
        }
    }

    private static T[,] RemoveColumn<T>(T[,] matrix, int k)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        if (rows == 0 || columns == 0)
        {
            throw new InvalidOperationException(
                "Нельзя удалить столбец из пустого массива.");
        }

        if (k < 1 || k > columns)
        {
            throw new ArgumentOutOfRangeException(
                nameof(k),
                "Столбца с таким номером не существует.");
        }

        T[,] result = new T[rows, columns - 1];

        for (int i = 0; i < rows; i++)
        {
            int newColumn = 0;

            for (int j = 0; j < columns; j++)
            {
                if (j == k - 1)
                    continue;

                result[i, newColumn] = matrix[i, j];
                newColumn++;
            }
        }

        return result;
    }

    // =========================================
    // РВАНЫЙ МАССИВ
    // =========================================

    private static void HandleJaggedArray()
    {
        int rows = ReadString.ReadPositiveValue<int>(
            "Количество исходных строк: ");

        int[][] array = new int[rows][];

        FillJaggedArray(array);

        Console.WriteLine("\nИсходный рваный массив:");
        ArrayIO.PrintArray(array);

        int k = ReadString.ReadValue<int>(
            "Количество строк для добавления: ");

        if (k < 0)
        {
            Console.WriteLine(
                "Ошибка: количество добавляемых строк не может быть отрицательным.");
            return;
        }

        Console.WriteLine("1. Ввод новых строк вручную");
        Console.WriteLine("2. Заполнение новых строк случайными числами");

        int mode = ReadString.ReadValue<int>("Выбор: ");

        try
        {
            array = AddRows(array, k, mode == 2);

            Console.WriteLine("\nРваный массив после добавления строк:");
            ArrayIO.PrintArray(array);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void FillJaggedArray<T>(T[][] array)
        where T : System.Numerics.INumber<T>
    {
        Console.WriteLine("1. Заполнить вручную");
        Console.WriteLine("2. Заполнить случайными числами");

        int choice = ReadString.ReadValue<int>("Выбор: ");

        switch (choice)
        {
            case 1:
                ArrayIO.FillArrayWithInput(array);
                break;
            case 2:
                ArrayIO.FillArrayWithRandom(array);
                break;
            default:
                Console.WriteLine(
                    "Неверный выбор. Выполняется ручное заполнение.");
                ArrayIO.FillArrayWithInput(array);
                break;
        }
    }

    private static T[][] AddRows<T>(
        T[][] array,
        int k,
        bool randomFill = false)
        where T : System.Numerics.INumber<T>
    {
        if (k < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(k),
                "Количество добавляемых строк не может быть отрицательным.");
        }

        if (k == 0)
            return array;

        T[][] result = new T[array.Length + k][];

        for (int i = 0; i < array.Length; i++)
            result[i] = array[i];

        Random random = new Random();

        for (int i = array.Length; i < result.Length; i++)
        {
            int size = ReadString.ReadPositiveValue<int>(
                $"Количество элементов в новой строке {i + 1}: ");

            if (randomFill)
            {
                result[i] = new T[size];

                for (int j = 0; j < size; j++)
                {
                    result[i][j] =
                        T.CreateChecked(random.Next(1, 100));
                }
            }
            else
            {
                while (true)
                {
                    T[] sequence = ReadString.ReadSequence<T>(
                        $"Введите {size} чисел через пробел:");

                    if (sequence.Length == size)
                    {
                        result[i] = sequence;
                        break;
                    }

                    Console.WriteLine(
                        "Количество элементов не совпадает с размером строки.");
                }
            }
        }

        return result;
    }

    // =========================================
    // ОБРАБОТКА СТРОКИ
    // =========================================

    private static void HandleString()
    {
        string[] testStrings =
        {
            "В лесу родилась елочка! В лесу она росла. " +
            "Зимой и летом стройная, зеленая была!"
        };

        Console.WriteLine("1. Ввести строку с клавиатуры");
        Console.WriteLine("2. Использовать тестовую строку");

        int choice = ReadString.ReadValue<int>("Выбор: ");

        string input;

        switch (choice)
        {
            case 1:
                Console.WriteLine("Введите строку:");
                input = Console.ReadLine() ?? string.Empty;
                break;

            case 2:
                input = testStrings[0];
                break;

            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }

        Console.WriteLine("\nИсходная строка:");
        Console.WriteLine(input);

        string result = RemoveFirstAndLastWord(input);

        Console.WriteLine("\nРезультат:");
        Console.WriteLine(result);
    }

    private static string RemoveFirstAndLastWord(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        MatchCollection sentences = Regex.Matches(
            text,
            @"[^.!?]*[.!?]+|[^.!?]+$");

        string[] result = sentences
            .Cast<Match>()
            .Select(match => ProcessSentence(match.Value))
            .Where(sentence => !string.IsNullOrWhiteSpace(sentence))
            .ToArray();

        return string.Join(" ", result);
    }

    private static string ProcessSentence(string sentence)
    {
        Match ending = Regex.Match(sentence, @"[.!?]+$");

        string punctuation = ending.Success ? ending.Value : "";

        string body = ending.Success
            ? sentence.Substring(0, ending.Index)
            : sentence;

        MatchCollection words = Regex.Matches(
            body,
            @"[\p{L}\p{N}]+(?:[-'][\p{L}\p{N}]+)*");

        if (words.Count <= 2)
            return punctuation;

        int start = words[0].Index + words[0].Length;
        int end = words[words.Count - 1].Index;

        string result = body.Substring(start, end - start).Trim();

        result = Regex.Replace(result, @"\s+", " ");
        result = Regex.Replace(result, @"\s+([,;:])", "$1");

        if (result.Length > 0)
        {
            result = char.ToUpper(result[0]) + result.Substring(1);
        }

        return result + punctuation;
    }
}
