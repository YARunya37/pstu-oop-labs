namespace Polities.Tests;

public class PolityTests
{
    // Метод_Сценарий_ОжидаемоеДействие(глагол)

    // КОНСТРУКТОРЫ

    [Fact]
    public void Constructor_NoParams_SetsDefaultValues()
    {
        Polity polity = new Polity();

        Assert.Equal("Не задано", polity.Name);

        Assert.Equal("Не задано", polity.Capital);

        Assert.Equal(1u, polity.Population);

        Assert.Equal(1.0, polity.Area);
    }

    [Fact]
    public void Constructor_ValidValues_SetsAllValues()
    {
        Polity polity = new Polity("Страна", "Столица", 10000u, 1000000.0);

        Assert.Equal("Страна", polity.Name);
        Assert.Equal("Столица", polity.Capital);
        Assert.Equal(10000u, polity.Population);
        Assert.Equal(1000000.0, polity.Area);
    }

    [Theory]
    [InlineData("", "Столица", 100u, 100.0)]
    [InlineData("Страна", "", 100u, 100.0)]
    [InlineData("Страна", "Столица", 0u, 100.0)]
    [InlineData("Страна", "Столица", 100u, 0.0)]
    public void Constructor_InvalidArgument_ThrowsArgumentException(
    string name, string capital, uint population, double area)
    {
        ArgumentException? caught = null;
        try
        {
            Polity polity = new Polity(name, capital, population, area);
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
    }

    [Fact]
    public void CopyConstructor_ValidOther_CopiesValue()
    {
        Polity other = new Polity("Страна", "Столица", 10000u, 1000000.0);

        Polity copy = new Polity(other);

        Assert.Equal("Страна", copy.Name);
        Assert.Equal("Столица", copy.Capital);
        Assert.Equal(10000u, copy.Population);
        Assert.Equal(1000000.0, copy.Area);
    }

    [Fact]
    public void CopyConstructor_InvalidOther_ThrowsArgumentNullException()
    {
        ArgumentNullException? caught = null;

        try
        {
            Polity polity = new Polity(null);
        }
        catch (ArgumentNullException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
    }

    // СВОЙСТВА

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Name_InvalidValue_ThrowsArgumentException(string? name)
    {
        Polity polity = new Polity();

        ArgumentException? caught = null;
        try
        {
            polity.Name = name;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Название государства не может быть пустым.", caught.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Capital_InvalidValue_ThrowsArgumentException(string? capital)
    {
        Polity polity = new Polity();

        ArgumentException? caught = null;
        try
        {
            polity.Capital = capital;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Название столицы не может быть пустым.", caught.Message);
    }

    [Theory]
    [InlineData(0u)]
    public void Population_InvalidValue_ThrowsArgumentException(uint population)
    {
        Polity polity = new Polity();

        ArgumentException? caught = null;
        try
        {
            polity.Population = population;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Численность населения не может быть меньше хотя бы одного человека", caught.Message);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(double.PositiveInfinity)]
    public void Area_InvalidValue_ThrowsArgumentException(double area)
    {
        Polity polity = new Polity();

        ArgumentException? caught = null;
        try
        {
            polity.Area = area;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Площадь должна быть конечным положительным числом", caught.Message);
    }

    // EQUALS и GETHASHCODE
    [Fact]
    public void Equals_SameObject_ReturnsTrue()
    {
        Polity polity = new Polity();

        Assert.True(polity.Equals(polity));
    }

    [Fact]
    public void Equals_EqualValues_ReturnsTrue()
    {
        Polity polity = new Polity("Страна", "Столица", 100u, 100.0);
        Polity toCompare = new Polity("Страна", "Столица", 100u, 100.0);

        Assert.True(polity.Equals(toCompare));
    }

    [Fact]
    public void Equals_NotPolity_ReturnsFalse()
    {
        Polity polity = new Polity();

        Assert.False(polity.Equals(new object()));
    }

    [Fact]
    public void Equals_DifferentRuntimeTypes_ReturnsFalse()
    {
        Polity polity = new Polity("Страна", "Столица", 10000u, 1000000.0);
        TestDerivedPolity derived = new TestDerivedPolity("Страна", "Столица", 10000u, 1000000.0);

        Assert.False(polity.Equals(derived));
        Assert.False(derived.Equals(polity));
    }

    [Theory]
    [InlineData("Другое", "Столица", 100u, 100.0)]
    [InlineData("Страна", "Другое", 100u, 100.0)]
    [InlineData("Страна", "Столица", 200u, 100.0)]
    [InlineData("Страна", "Столица", 100u, 200.0)]
    public void Equals_DifferentFields_ReturnsFalse(string name, string capital, uint population, double area)
    {
        Polity expected = new Polity("Страна", "Столица", 100u, 100.0);
        Polity other = new Polity(name, capital, population, area);

        Assert.False(expected.Equals(other));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        Polity polity = new Polity();

        Assert.False(polity.Equals(null));
    }

    // Равные объекты должны получать одинаковый хеш
    [Fact]
    public void GetHashCode_EqualObjects_SameHash()
    {
        Polity a = new Polity("Страна", "Столица", 100u, 100.0);
        Polity b = new Polity("Страна", "Столица", 100u, 100.0);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}

internal class TestDerivedPolity : Polity
{
    public TestDerivedPolity() : base() { }
    public TestDerivedPolity(string name, string capital, uint population, double area)
    : base(name, capital, population, area) { }
}