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


    // Часть 3. 
    public static void ShowTask3()
    {
        // Первая часть
        ShowSortAndBinary();

        // Вторая часть - демонстрация работы наследников IInit
        ShowInitable();

        // ==========================================================
        // БЛОК 3. Clone / ShallowCopy — после реализации ICloneable
        // ==========================================================
        // Console.WriteLine("\n========== БЛОК 3. CLONE / SHALLOWCOPY ==========\n");
        // ...
    }

    private static void ShowSortAndBinary()
    {
        
        // ==========================================================
        // БЛОК 1. СОРТИРОВКА И БИНАРНЫЙ ПОИСК
        // ==========================================================
        Console.WriteLine("========== БЛОК 1. СОРТИРОВКА И БИНАРНЫЙ ПОИСК ==========\n");

        // Разные имена и населения — чтобы сортировка по Name и по Population
        // давали разные порядки, и это было видно.
        Polity[] states =
        {
        new Polity("Зета",   "Столица1", 5_000_000u, 500_000.0),
        new Republic("Альфа", "Столица2", 2_000_000u, 200_000.0,
            "Иванов", 5, "Сенат", true),
        new Monarchy("Гамма", "Столица3", 8_000_000u, 800_000.0,
            "Александр", "Король", "Романовы", true),
        new Kingdom("Бета",  "Столица4", 1_000_000u, 100_000.0,
            "Николай", "Император", "Романовы", true,
            "Зимний дворец", 10u, 500_000.0),
        new Polity("Дельта", "Столица5", 3_000_000u, 300_000.0)
    };

        Console.WriteLine("Исходный массив:");
        foreach (Polity p in states)
            Console.WriteLine($"{p.Name,-10} население: {p.Population,-12:N0} площадь: {p.Area:N2}");

        // ----------------------------------------------------------
        // 1a. Сортировка через IComparable (естественный порядок = Name)
        // ----------------------------------------------------------
        Console.WriteLine("\n--- Сортировка через IComparable (Array.Sort по Name) ---");
        Array.Sort(states);
        foreach (Polity p in states)
            Console.WriteLine($"  {p.Name}");

        // ----------------------------------------------------------
        // 1b. Бинарный поиск по Name (через IComparable)
        //     ВАЖНО: массив уже отсортирован по Name тем же критерием.
        //     target — объект с нужным Name, остальные поля-заглушки.
        // ----------------------------------------------------------
        Console.WriteLine("\n--- Бинарный поиск по Name ---");

        Polity targetName = new Polity("Гамма", "заглушка", 1u, 1.0);
        int idxName = Array.BinarySearch(states, targetName);
        if (idxName >= 0)
            Console.WriteLine($"\"Гамма\" найдено на позиции {idxName} — {states[idxName].Name}");
        else
            Console.WriteLine($"\"Гамма\" не найдено (индекс: {idxName})");

        Polity missingName = new Polity("Омега", "заглушка", 1u, 1.0);
        int missingNameIdx = Array.BinarySearch(states, missingName);
        if (missingNameIdx < 0)
            Console.WriteLine($"  \"Омега\" не найдено. Точка вставки: {~missingNameIdx}");

        // ----------------------------------------------------------
        // 1c. Сортировка через IComparer (другой критерий = Population)
        // ----------------------------------------------------------
        Console.WriteLine("\n--- Сортировка через IComparer (Array.Sort по Population) ---");
        PopulationComparer comparer = new PopulationComparer();
        Array.Sort(states, comparer);
        foreach (Polity p in states)
            Console.WriteLine($"  {p.Name,-10} население: {p.Population:N0}");

        // ----------------------------------------------------------
        // 1d. Бинарный поиск по Population (через тот же IComparer)
        //     ВАЖНО: Sort и BinarySearch должны использовать ОДИН компаратор.
        // ----------------------------------------------------------
        Console.WriteLine("\n--- Бинарный поиск по Population ---");

        Polity targetPop = new Polity("заглушка", "заглушка", 3_000_000u, 1.0);
        int idxPop = Array.BinarySearch(states, targetPop, comparer);
        if (idxPop >= 0)
            Console.WriteLine($"  население 3 000 000 найдено на позиции {idxPop} — {states[idxPop].Name}");
        else
            Console.WriteLine($"  население 3 000 000 не найдено");

        Polity missingPop = new Polity("заглушка", "заглушка", 7_000_000u, 1.0);
        int missingPopIdx = Array.BinarySearch(states, missingPop, comparer);
        if (missingPopIdx < 0)
            Console.WriteLine($"  население 7 000 000 не найдено. Точка вставки: {~missingPopIdx}");
    }

    private static void ShowInitable()
    {
        
        // ==========================================================
        // БЛОК 2. IInit[] — группировка по поведению
        // ==========================================================
        Console.WriteLine("\n========== БЛОК 2. IINIT[] ==========\n");
        Console.WriteLine("IInit задаёт поведение (Init, RandomInit), а не «вид государства».");
        Console.WriteLine("В одном массиве — объекты иерархии Polity и Organization (вне иерархии).");
        Console.WriteLine("Обрабатываются одинаково — через интерфейс, без знания конкретного типа.\n");

        // Массив IInit: элементы разного типа (Polity/Republic/Monarchy/Kingdom/Organization),
        // но все реализуют IInit. Компилятор проверяет только контракт интерфейса.
        IInit[] items =
        {
            new Polity(),
            new Republic(),
            new Monarchy(),
            new Kingdom(),
            new Organization()
        };

        // 1. RandomInit через интерфейс — без is/as/кастов
        Console.WriteLine("--- RandomInit() через IInit ---");
        for (int i = 0; i < items.Length; i++)
        {
            items[i].RandomInit();
            Console.WriteLine($"[{i}] {items[i].GetType().Name,-15} → {items[i]}");
        }

        // 2. Просмотр всего массива через ToString.
        //    IInit не содержит Show — поэтому используем ToString (наследуется от object),
        //    доступный через IInit-ссылку. У Polity — sealed override с ToStringExtra,
        //    у Organization — свой override. Один механизм для всех.
        Console.WriteLine("\n--- Просмотр массива через ToString (после Init) ---");
        foreach (IInit x in items)
            Console.WriteLine($"{x}");
    }


}
