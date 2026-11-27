using Polities;

namespace Program;

internal static class Queries
{
    // ЗАПРОС 1. Имена всех глав государств.
    // Без RTTI: используется виртуальный метод GetHeadOfState().
    // Параметров нет.
    public static void PrintHeadsOfState(Polity[] states)
    {
        foreach (Polity p in states)
            Console.WriteLine($"  {p.Name} - {p.GetHeadOfState()}");
    }

    // ЗАПРОС 2. Количество президентских республик.
    // RTTI: is-pattern. Параметров нет.
    public static int CountPresidentialRepublics(Polity[] states)
    {
        int count = 0;
        foreach (Polity p in states)
            if (p is Republic r && r.IsPresidential)
                count++;
        return count;
    }

    // ЗАПРОС 3. Названия республик с заданным парламентом.
    // RTTI: is-pattern + метод IsParliamentNamed.
    // Параметр — название парламента.
    public static void PrintRepublicsByParliament(Polity[] states, string parliament)
    {
        bool found = false;
        foreach (Polity p in states)
        {
            if (p is Republic r && r.IsParliamentNamed(parliament))
            {
                p.Show();
                Console.WriteLine();
                found = true;
            }
        }
        if (!found)
            Console.WriteLine("Республик с таким парламентом не найдено.");
    }

    // ЗАПРОС 4. Названия монархий с заданным титулом.
    // RTTI: is-pattern + сравнение строк без учёта регистра.
    // Параметр — титул.
    public static void PrintMonarchiesByTitle(Polity[] states, string title)
    {
        bool found = false;
        foreach (Polity p in states)
        {
            if (p is Monarchy m &&
                string.Equals(m.Title, title, StringComparison.OrdinalIgnoreCase))
            {
                p.Show();
                Console.WriteLine();
                found = true;
            }
        }
        if (!found)
            Console.WriteLine("  Монархий с таким титулом не найдено.");
    }

    // ЗАПРОС 5. Среднее число герцогств в королевствах.
    // RTTI: as + агрегация. Параметров нет.
    public static double AverageDuchiesInKingdoms(Polity[] states)
    {
        double sum = 0;
        int count = 0;
        foreach (Polity p in states)
        {
            Kingdom k = p as Kingdom;
            if (k != null)
            {
                sum += k.DuchiesCount;
                count++;
            }
        }
        return count > 0 ? sum / count : 0;
    }
}