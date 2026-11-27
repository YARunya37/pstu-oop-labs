using System.Collections;
using Polities;

namespace Program;

public class PopulationComparer : IComparer
{
    // Отличие от обычной реализации в получении доступа:
    // в текущем варианте мы имеем доступ через интерфейс IComparer
    // если бы переопределили как в Polity, то имели бы доступ и через PopulationSortComparer, и интерфейс
    int IComparer.Compare(object? x, object? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        Polity polity1 = (Polity)x;
        Polity polity2 = (Polity)y;
        return polity1.Population.CompareTo(polity2.Population);
    }
}
