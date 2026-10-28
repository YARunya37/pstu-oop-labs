using System.Numerics;

namespace IOUtils
{
    public static class ArrayIO
    {
        // =========================
        // ОДНОМЕРНЫЙ МАССИВ
        // =========================

        public static void FillArrayWithRandom<T>(T[] array)
    where T : INumber<T>
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = T.CreateChecked(random.Next(1, 100));
            }
        }

        public static void FillArrayWithInput<T>(T[] array)
            where T : INumber<T>
        {
            T[] sequence;

            do
            {
                sequence = ReadString.ReadSequence<T>($"Введите {array.Length} элементов массива через пробел:");

                if (sequence.Length != array.Length)
                {
                    Console.WriteLine("Неверный ввод! Количество элементов должно соответствовать размеру массива.");
                }
            }
            while (sequence.Length != array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sequence[i];
            }
        }

        public static void PrintArray<T>(T[] array)
        {
            if (array.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]}\t");
            }

            Console.WriteLine();
        }

        // =========================
        // ДВУМЕРНЫЙ МАССИВ
        // =========================

        public static void FillArrayWithRandom<T>(T[,] array)
            where T : INumber<T>
        {
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = T.CreateChecked(random.Next(1, 100));
                }
            }
        }

        public static void FillArrayWithInput<T>(T[,] array)
            where T : INumber<T>
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                T[] sequence;

                do
                {
                    sequence = ReadString.ReadSequence<T>();

                    if (sequence.Length != array.GetLength(1))
                    {
                        Console.WriteLine(
                            "Неверный ввод! Количество элементов в строке " +
                            "должно соответствовать размеру массива.");
                    }

                } while (sequence.Length != array.GetLength(1));

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = sequence[j];
                }
            }
        }

        public static void PrintArray<T>(T[,] array)
        {
            if(array.GetLength(0) == 0 || array.GetLength(1) == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "\t");
                }

                Console.WriteLine();
            }
        }


        // =========================
        // РВАНЫЙ МАССИВ
        // =========================

        public static void FillArrayWithRandom<T>(T[][] array)
            where T : INumber<T>
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                int size = ReadString.ReadPositiveValue<int>(
                    "Введите количество элементов в строке " +
                    (i + 1) + ": ");

                array[i] = new T[size];

                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = T.CreateChecked(random.Next(1, 100));
                }
            }
        }

        public static void FillArrayWithInput<T>(T[][] array)
            where T : INumber<T>
        {
            for (int i = 0; i < array.Length; i++)
            {
                T[] sequence;

                Console.WriteLine(
                    "Введите элементы строки " + (i + 1) + ":");

                sequence = ReadString.ReadSequence<T>();

                array[i] = new T[sequence.Length];

                for (int j = 0; j < sequence.Length; j++)
                {
                    array[i][j] = sequence[j];
                }
            }
        }

        public static void PrintArray<T>(T[][] array)
        {
            if(array.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }

            bool isEmpty = true;
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i].Length != 0)
                {
                    isEmpty = false;
                }
                
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + "\t");
                }

                Console.WriteLine();
            }

            if (isEmpty)
            {
                Console.WriteLine("Массив пуст.");
            }
        }
    }

}