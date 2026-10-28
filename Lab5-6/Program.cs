
using System;
using System.Linq;
using System.Text.RegularExpressions;
using IOUtils;

public class Program
{
    private static int ReadChoice(string message, params int[] availableValues)
    {
        int value = 0;
        bool isValid = false;

        while (!isValid)
        {
            value = ReadString.ReadValue<int>(message);

            if (availableValues.Contains(value))
                isValid = true;
            else
                Console.WriteLine("Нет такого пункта меню.");
        }

        return value;
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

        bool correctAnswer = false;

        while (!correctAnswer)
        {
            int k = ReadString.ReadValue<int>("Номер столбца для удаления (нумерация с 1): ");

            try
            {
                matrix = RemoveColumn(matrix, k);

                Console.WriteLine("\nМассив после удаления столбца:");
                ArrayIO.PrintArray(matrix);
                correctAnswer = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    private static void FillMatrix<T>(T[,] matrix)
        where T : System.Numerics.INumber<T>
    {
        Console.WriteLine("1. Заполнить вручную");
        Console.WriteLine("2. Заполнить случайными числами");

        int choice = ReadChoice("Выбор: ", 1, 2);

        switch (choice)
        {
            case 1:
                ArrayIO.FillArrayWithInput(matrix);
                break;
            case 2:
                ArrayIO.FillArrayWithRandom(matrix);
                break;
        }
    }

    private static T[,] RemoveColumn<T>(T[,] matrix, int columnNumber)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        if (rows == 0 || columns == 0)
        {
            throw new InvalidOperationException("Нельзя удалить столбец из пустого массива.");
        }

        if (columnNumber < 1 || columnNumber > columns)
        {
            throw new ArgumentOutOfRangeException(nameof(columnNumber), "Столбца с таким номером не существует.");
        }

        T[,] result = new T[rows, columns - 1];

        for (int i = 0; i < rows; i++)
        {
            int newColumn = 0;

            for (int j = 0; j < columns; j++)
            {
                if (j == columnNumber - 1)
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
        int rows = ReadString.ReadPositiveValue<int>("Количество исходных строк: ");

        int[][] array = new int[rows][];

        FillJaggedArray(array);

        Console.WriteLine("\nИсходный рваный массив:");
        ArrayIO.PrintArray(array);

        int k = ReadString.ReadPositiveValue<int>("Количество строк для добавления: ");


        Console.WriteLine("1. Ввод новых строк вручную");
        Console.WriteLine("2. Заполнение новых строк случайными числами");

        bool correctAnswer = false;
        while (!correctAnswer)
        {
            int mode = ReadChoice("Выбор: ", 1, 2);

            try
            {
                array = AddRows(array, k, mode == 2);

                Console.WriteLine("\nРваный массив после добавления строк:");
                ArrayIO.PrintArray(array);
                correctAnswer = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    private static void FillJaggedArray<T>(T[][] array)
        where T : System.Numerics.INumber<T>
    {
        Console.WriteLine("1. Заполнить вручную");
        Console.WriteLine("2. Заполнить случайными числами");


        int choice = ReadChoice("Выбор: ", 1, 2);

        switch (choice)
        {
            case 1:
                ArrayIO.FillArrayWithInput(array);
                break;
            case 2:
                ArrayIO.FillArrayWithRandom(array);
                break;
        }
    }

    private static T[][] AddRows<T>(T[][] array, int rowsNumber, bool randomFill = false)
        where T : System.Numerics.INumber<T>
    {
        if (rowsNumber < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rowsNumber),
                "Количество добавляемых строк не может быть отрицательным.");
        }

        if (rowsNumber == 0)
            return array;

        T[][] result = new T[array.Length + rowsNumber][];

        // Идём по строчкам и копируем ссылки на массивы в каждой строке
        for (int i = 0; i < array.Length; i++)
            result[i] = array[i];

        Random random = new Random();

        // Добавляем новые строки
        for (int i = array.Length; i < result.Length; i++)
        {
            int size = ReadString.ReadPositiveValue<int>($"Количество элементов в новой строке {i + 1}: ");

            result[i] = new T[size];

            // Заполнение массива в строке
            if (randomFill)
            {
                ArrayIO.FillArrayWithRandom<T>(result[i]);
            }
            else
            {
                ArrayIO.FillArrayWithInput<T>(result[i]);
            }
        }

        return result;
    }

    // =========================================
    // ОБРАБОТКА СТРОКИ
    // =========================================

    private static void HandleString()
    {
        string testString =
            "В лесу родилась елочка! В лесу она росла. " +
            "Зимой и летом стройная, зеленая была!";

        string input = "";
        bool inputReceived = false;

        while (!inputReceived)
        {
            Console.WriteLine("1. Ввести строку с клавиатуры");
            Console.WriteLine("2. Использовать тестовую строку");

            int choice = ReadChoice("Выбор: ", 1, 2);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введите строку:");
                    input = Console.ReadLine() ?? string.Empty;
                    inputReceived = input != "";

                    if (!inputReceived)
                    {
                        Console.WriteLine("Была введена пустая строка\n");
                    }

                    break;

                case 2:
                    input = testString;
                    inputReceived = true;
                    break;
            }
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

    public static void Main()
    {
        bool programWorks = true;

        while (programWorks)
        {
            Console.WriteLine("\n========= ГЛАВНОЕ МЕНЮ =========");
            Console.WriteLine("1. Двумерный массив");
            Console.WriteLine("2. Рваный массив");
            Console.WriteLine("3. Обработка строки");
            Console.WriteLine("0. Выход");

            int choice = ReadChoice("Ваш выбор: ", 0, 1, 2, 3);

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
                    programWorks = false;
                    break;
            }
        }
    }
}
