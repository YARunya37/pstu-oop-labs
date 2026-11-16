using Polities;
using IOUtils;
namespace Program;

public static class Demo
{
    // Сравнение обычных и виртуальных функций
    public static void ShowTask1()
    {
        // Массив из всех типов иерархии
        Polity[] states =
        {
            new Polity(),
            new Republic(),
            new Monarchy(),
            new Kingdom()
        };
        foreach (Polity p in states)
            p.RandomInit();

        // Просмотр ОБЫЧНОЙ (невиртуальной) функцией.
        // Статическое связывание: для всех вызывается Polity.ShowBaseInfo.
        Console.WriteLine("=== Обычная (невиртуальная) функция ===");
        foreach (Polity p in states)
        {
            p.ShowRegularInfo();
            Console.WriteLine();
        }

        // Просмотр ВИРТУАЛЬНОЙ функцией.
        // Динамическое связывание: у каждого своя override-версия.
        Console.WriteLine("\n=== Виртуальная функция ===");
        foreach (Polity p in states)
        {
            p.ShowVirtualInfo();
            Console.WriteLine();
        }

    }

    // RTTI, запросы
    public static void ShowTask2()
    {

        // Массив из всех типов иерархии
        Polity[] states = new Polity[100];
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = (i % 4) switch
            {
                0 => new Polity(),
                1 => new Republic(),
                2 => new Monarchy(),
                _ => new Kingdom()
            };
            states[i].RandomInit();
        }


        // Запрос 1 — без параметров
        Console.WriteLine("=== 1. Имена всех глав государств ===");
        Queries.PrintHeadsOfState(states);

        // Запрос 2 — без параметров
        Console.WriteLine("\n=== 2. Количество президентских республик ===");
        int presCount = Queries.CountPresidentialRepublics(states);
        Console.WriteLine($"Президентских республик: {presCount}");

        // Запрос 3 — параметр с клавиатуры (парламент)
        Console.WriteLine("\n=== 3. Республики с заданным парламентом ===");
        Console.WriteLine("Допустимые названия парламентов:");
        Console.WriteLine("Национальное собрание, Конгресс, Сенат, Парламент,");
        Console.WriteLine("Государственная дума, Народное собрание");
        string parliament = ReadString.ReadStringValue(
            "  Введите название парламента: ",
            "  Название парламента не может быть пустым.");
        Queries.PrintRepublicsByParliament(states, parliament);

        // Запрос 4 — параметр с клавиатуры (титул)
        Console.WriteLine("\n=== 4. Монархии с заданным титулом ===");
        Console.WriteLine("Допустимые титулы:");
        Console.WriteLine("Король, Император, Султан, Царь, Хан, Шах");
        string title = ReadString.ReadStringValue(
            "Введите титул: ",
            "Титул не может быть пустым.");

        Queries.PrintMonarchiesByTitle(states, title);

        // Запрос 5 — без параметров
        Console.WriteLine("\n=== 5. Среднее число герцогств в королевствах ===");
        double avg = Queries.AverageDuchiesInKingdoms(states);
        Console.WriteLine($"Среднее число герцогств: {avg:F2}");
    }
}
