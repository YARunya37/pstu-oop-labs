using Polities;
using Program;

namespace Program.Tests;

public class QueriesTests
{
    // ВСПОМОГАТЕЛЬНЫЕ ПОЛЯ

    private static Republic Republic(bool isPresidential) =>
        new Republic("Республика", "Столица", 100u, 100.0,
            "Президент", 5, "Сенат", isPresidential);

    private static Kingdom Kingdom(uint duchiesCount) =>
        new Kingdom("Королевство", "Столица", 100u, 100.0,
            "Монарх", "Король", "Династия", true,
            "Резиденция", duchiesCount, 1000.0);

    // COUNT PRESIDENTIAL REPUBLICS

    [Fact]
    public void CountPresidentialRepublics_EmptyArray_ReturnsZero()
    {
        Polity[] states = Array.Empty<Polity>();

        int count = Queries.CountPresidentialRepublics(states);

        Assert.Equal(0, count);
    }

    [Fact]
    public void CountPresidentialRepublics_NoRepublics_ReturnsZero()
    {
        Polity[] states =
        {
            new Polity("Государство", "Столица", 100u, 100.0),
            new Monarchy("Монархия", "Столица", 100u, 100.0,
                "Монарх", "Король", "Династия", true)
        };

        int count = Queries.CountPresidentialRepublics(states);

        Assert.Equal(0, count);
    }

    [Fact]
    public void CountPresidentialRepublics_NoPresidentialRepublics_ReturnsZero()
    {
        Polity[] states =
        {
            Republic(isPresidential: false),
            Republic(isPresidential: false)
        };

        int count = Queries.CountPresidentialRepublics(states);

        Assert.Equal(0, count);
    }

    [Fact]
    public void CountPresidentialRepublics_AllPresidential_ReturnsAllCount()
    {
        Polity[] states =
        {
            Republic(isPresidential: true),
            Republic(isPresidential: true),
            Republic(isPresidential: true)
        };

        int count = Queries.CountPresidentialRepublics(states);

        Assert.Equal(3, count);
    }

    [Fact]
    public void CountPresidentialRepublics_MixedArray_ReturnsOnlyPresidential()
    {
        Polity[] states =
        {
            new Polity("Государство", "Столица", 100u, 100.0),   // не республика
            Republic(isPresidential: true),
            Republic(isPresidential: false),
            new Monarchy("Монархия", "Столица", 100u, 100.0,     // не республика
                "Монарх", "Король", "Династия", true),          
            Republic(isPresidential: true),
            Kingdom(5u)                                          // не республика
        };

        int count = Queries.CountPresidentialRepublics(states);

        Assert.Equal(2, count);
    }


    [Fact]
    public void AverageDuchiesInKingdoms_EmptyArray_ReturnsZero()
    {
        Polity[] states = Array.Empty<Polity>();

        double avg = Queries.AverageDuchiesInKingdoms(states);

        Assert.Equal(0.0, avg);
    }

    [Fact]
    public void AverageDuchiesInKingdoms_NoKingdoms_ReturnsZero()
    {
        Polity[] states =
        {
            new Polity("Государство", "Столица", 100u, 100.0),
            new Republic("Республика", "Столица", 100u, 100.0,
                "Президент", 5, "Сенат", true),
            new Monarchy("Монархия", "Столица", 100u, 100.0,
                "Монарх", "Король", "Династия", true)
        };

        double avg = Queries.AverageDuchiesInKingdoms(states);

        Assert.Equal(0.0, avg);
    }

    [Fact]
    public void AverageDuchiesInKingdoms_SingleKingdom_ReturnsItsCount()
    {
        Polity[] states = { Kingdom(5u) };

        double avg = Queries.AverageDuchiesInKingdoms(states);

        Assert.Equal(5.0, avg);
    }

    [Fact]
    public void AverageDuchiesInKingdoms_MultipleKingdoms_ReturnsAverage()
    {
        Polity[] states =
        {
            Kingdom(2u),
            Kingdom(4u),
            Kingdom(6u)
        };

        double avg = Queries.AverageDuchiesInKingdoms(states);

        // (2 + 4 + 6) / 3 = 4
        Assert.Equal(4.0, avg);
    }

    [Fact]
    public void AverageDuchiesInKingdoms_MixedArray_IgnoresNonKingdoms()
    {
        Polity[] states =
        {
            new Polity("Государство", "Столица", 100u, 100.0),
            Kingdom(3u),
            new Republic("Республика", "Столица", 100u, 100.0,
                "Президент", 5, "Сенат", true),
            Kingdom(7u),
            new Monarchy("Монархия", "Столица", 100u, 100.0,
                "Монарх", "Король", "Династия", true)
        };

        double avg = Queries.AverageDuchiesInKingdoms(states);

        // (3 + 7) / 2 = 5 — не считая Polity/Republic/Monarchy
        Assert.Equal(5.0, avg);
    }
}