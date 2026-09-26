using System;

class Program
{
    // =========================
    // ВВОД ДАННЫХ
    // =========================

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
        }
        while (!isParsed);

        Console.WriteLine();

        return value;
    }

    static bool IsPositive(int value)
    {
        return value > 0;
    }

    static int GetPositiveInt(string message = "")
    {
        int value = GetInt(message);

        while (!IsPositive(value))
        {
            Console.WriteLine("Ошибка ввода! Значение должно быть больше нуля!");
            value = GetInt(message);
        }

        return value;
    }


    // =========================
    // ДВУМЕРНЫЙ МАССИВ
    // =========================

    static void FillArray(int[,] array)
    {
        Random random = new Random();

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = random.Next(1, 100);
            }
        }
    }

    static void PrintArray(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    static int[,] DeleteColumn(int[,] array, int deleteIndex)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);

        int[,] newArray = new int[rows, columns - 1];

        for (int i = 0; i < rows; i++)
        {
            int newJ = 0;

            for (int j = 0; j < columns; j++)
            {
                if (j != deleteIndex)
                {
                    newArray[i, newJ] = array[i, j];
                    newJ++;
                }
            }
        }

        return newArray;
    }


    // =========================
    // РВАНЫЙ МАССИВ
    // =========================

    static void FillArray(int[][] array)
    {
        Random random = new Random();

        for (int i = 0; i < array.Length; i++)
        {
            int size = GetPositiveInt("Введите количество элементов в строке " + (i + 1) + ": ");

            array[i] = new int[size];

            for (int j = 0; j < array[i].Length; j++)
            {
                array[i][j] = random.Next(1, 100);
            }
        }
    }

    static void PrintArray(int[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                Console.Write(array[i][j] + "\t");
            }

            Console.WriteLine();
        }
    }

    static int[][] AddRows(int[][] array, int k)
    {
        int[][] newArray = new int[array.Length + k][];

        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }

        Random random = new Random();

        for (int i = array.Length; i < newArray.Length; i++)
        {
            int size = GetPositiveInt("Введите количество элементов в новой строке " + (i + 1) + ": ");

            newArray[i] = new int[size];

            for (int j = 0; j < newArray[i].Length; j++)
            {
                newArray[i][j] = random.Next(1, 100);
            }
        }

        return newArray;
    }

    static void Main()
    {
        Console.WriteLine(@"
========================================
               ЗАДАНИЕ 1
========================================
Сформировать динамический двумерный
массив, заполнить его и вывести на печать.

Удалить столбец с номером K.
========================================
");

        int n = GetPositiveInt("Введите количество строк: ");
        int m = GetPositiveInt("Введите количество столбцов: ");

        int[,] array = new int[n, m];

        FillArray(array);

        Console.WriteLine("Исходный массив:");
        PrintArray(array);

        int k = GetPositiveInt("\nВведите номер столбца K для удаления (1-" + m + "): ");

        while (k > m)
        {
            Console.WriteLine("Ошибка! Номер столбца должен быть от 1 до " + m + ".");

            k = GetPositiveInt("Введите номер столбца K для удаления (1-" + m + "): ");
        }

        // Переходим от номера столбца к интексу
        k--;

        array = DeleteColumn(array, k);

        Console.WriteLine("\nМассив после удаления столбца:");
        PrintArray(array);


        Console.WriteLine(@"

========================================
               ЗАДАНИЕ 2
========================================
Сформировать динамический рваный массив,
заполнить его и вывести на печать.

Добавить K строк в конец массива.
========================================
");

        int rows = GetPositiveInt("Введите количество строк: ");

        int[][] jaggedArray = new int[rows][];

        FillArray(jaggedArray);

        Console.WriteLine("Исходный рваный массив:");
        PrintArray(jaggedArray);

        int count = GetPositiveInt("\nВведите количество добавляемых строк K: ");

        jaggedArray = AddRows(jaggedArray, count);

        Console.WriteLine("\nРваный массив после добавления строк:");
        PrintArray(jaggedArray);
    }
}