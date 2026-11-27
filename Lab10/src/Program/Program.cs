using Polities;

namespace Program;

internal static class Program
{
    private static void Main()
    {
        // Задание 1
        Console.WriteLine("========== ЗАДАНИЕ 1 ==========\n");
        Demo.ShowTask1();
        Console.WriteLine("Нажмите enter, чтобы продолжить");
        Console.ReadLine();

        // ЗАДАНИЕ 2
        Console.WriteLine("========== ЗАДАНИЕ 2 ==========\n");
        Demo.ShowTask2();
        Console.WriteLine("Нажмите enter, чтобы продолжить");
        Console.ReadLine();

        // ЗАДАНИЕ 3
        Console.WriteLine("========== ЗАДАНИЕ 3 ==========\n");
        Demo.ShowTask3();
        Console.WriteLine("Нажмите enter, чтобы завершить");
        Console.ReadLine();

    }
}