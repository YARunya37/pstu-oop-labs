namespace Polities.Tests;

public class RepublicTests
{
    // КОНСТРУКТОРЫ

    [Fact]
    public void Constructor_NoParams_SetsDefaultValues()
    {
        Republic republic = new Republic();

        Assert.Equal("Не задано", republic.Name);
        Assert.Equal("Не задано", republic.Capital);
        Assert.Equal(1u, republic.Population);
        Assert.Equal(1.0, republic.Area);
        Assert.Equal("Не задано", republic.President);
        Assert.Equal(1, republic.PresidentTermYears);
        Assert.Equal("Не задано", republic.ParliamentName);
        Assert.False(republic.IsPresidential);

        Assert.NotNull(republic.Currency);
        Assert.Equal("Не задано", republic.Currency.Name);
        Assert.Equal("?", republic.Currency.Symbol);
    }

    [Fact]
    public void Constructor_BaseOnly_SetsRepublicDefaults()
    {
        Republic republic = new Republic("Страна", "Столица", 100u, 100.0);

        Assert.Equal("Страна", republic.Name);
        Assert.Equal("Не задано", republic.President);
        Assert.Equal(1, republic.PresidentTermYears);
        Assert.Equal("Не задано", republic.ParliamentName);
        Assert.False(republic.IsPresidential);
        Assert.NotNull(republic.Currency);
    }

    [Fact]
    public void Constructor_Full_SetsAllValues()
    {
        Republic republic = new Republic(
            "Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);

        Assert.Equal("Страна", republic.Name);
        Assert.Equal("Столица", republic.Capital);
        Assert.Equal(100u, republic.Population);
        Assert.Equal(100.0, republic.Area);
        Assert.Equal("Иванов", republic.President);
        Assert.Equal(5, republic.PresidentTermYears);
        Assert.Equal("Сенат", republic.ParliamentName);
        Assert.True(republic.IsPresidential);

        // Currency не параметризована — дефолт
        Assert.NotNull(republic.Currency);
        Assert.Equal("Не задано", republic.Currency.Name);
    }

    [Theory]
    [InlineData("", "Столица", 100u, 100.0, "Иванов", 5, "Сенат", true)]
    [InlineData("Страна", "", 100u, 100.0, "Иванов", 5, "Сенат", true)]
    [InlineData("Страна", "Столица", 0u, 100.0, "Иванов", 5, "Сенат", true)]
    [InlineData("Страна", "Столица", 100u, 0.0, "Иванов", 5, "Сенат", true)]
    [InlineData("Страна", "Столица", 100u, 100.0, "", 5, "Сенат", true)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Иванов", 0, "Сенат", true)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Иванов", 5, "", true)]
    public void Constructor_InvalidArgument_ThrowsArgumentException(
        string name, string capital, uint population, double area,
        string president, int presidentTermYears, string parliamentName, bool isPresidential)
    {
        ArgumentException? caught = null;
        try
        {
            Republic republic = new Republic(
                name, capital, population, area,
                president, presidentTermYears, parliamentName, isPresidential);
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
    }

    [Fact]
    public void CopyConstructor_ValidOther_CopiesAllValues()
    {
        Republic other = new Republic(
            "Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true)
        {
            Currency = new Currency("Доллар", "$")
        };

        Republic copy = new Republic(other);

        Assert.Equal("Страна", copy.Name);
        Assert.Equal("Столица", copy.Capital);
        Assert.Equal(100u, copy.Population);
        Assert.Equal(100.0, copy.Area);
        Assert.Equal("Иванов", copy.President);
        Assert.Equal(5, copy.PresidentTermYears);
        Assert.Equal("Сенат", copy.ParliamentName);
        Assert.True(copy.IsPresidential);

        // Currency deep-copy
        Assert.NotNull(copy.Currency);
        Assert.NotSame(other.Currency, copy.Currency);
        Assert.Equal("Доллар", copy.Currency.Name);
    }

    [Fact]
    public void CopyConstructor_Null_ThrowsArgumentNullException()
    {
        ArgumentNullException? caught = null;
        try
        {
            Republic republic = new Republic(null!);
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
    public void President_InvalidValue_ThrowsArgumentException(string? president)
    {
        Republic republic = new Republic();

        ArgumentException? caught = null;
        try
        {
            republic.President = president!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Имя президента не может быть пустым.", caught.Message);
    }

    [Fact]
    public void President_ValidValueWithSpaces_TrimsValue()
    {
        Republic republic = new Republic();
        republic.President = "  Иванов  ";

        Assert.Equal("Иванов", republic.President);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void PresidentTermYears_InvalidValue_ThrowsArgumentException(int term)
    {
        Republic republic = new Republic();

        ArgumentException? caught = null;
        try
        {
            republic.PresidentTermYears = term;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Срок президентских полномочий должен быть от 1 до 10 лет.", caught.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void PresidentTermYears_ValidValue_SetsValue(int term)
    {
        Republic republic = new Republic();
        republic.PresidentTermYears = term;

        Assert.Equal(term, republic.PresidentTermYears);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void ParliamentName_InvalidValue_ThrowsArgumentException(string? parliament)
    {
        Republic republic = new Republic();

        ArgumentException? caught = null;
        try
        {
            republic.ParliamentName = parliament!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Название парламента не может быть пустым.", caught.Message);
    }

    [Fact]
    public void ParliamentName_ValidValueWithSpaces_TrimsValue()
    {
        Republic republic = new Republic();
        republic.ParliamentName = "  Сенат  ";

        Assert.Equal("Сенат", republic.ParliamentName);
    }

    // ДОМЕННЫЕ МЕТОДЫ

    [Fact]
    public void GetHeadOfState_ReturnsPresident()
    {
        Republic republic = new Republic(
            "Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);

        Assert.Equal("Иванов", republic.GetHeadOfState());
    }

    [Theory]
    [InlineData("Сенат", true)]
    [InlineData("сенат", true)]
    [InlineData("СЕНАТ", true)]
    [InlineData("  Сенат  ", true)]
    [InlineData("Конгресс", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData(null, false)]
    public void IsParliamentNamed_VariousInputs_ReturnsExpected(string? query, bool expected)
    {
        Republic republic = new Republic(
            "Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);

        Assert.Equal(expected, republic.IsParliamentNamed(query!));
    }

    // EQUALS

    [Fact]
    public void Equals_SameObject_ReturnsTrue()
    {
        Republic republic = new Republic();

        Assert.True(republic.Equals(republic));
    }

    [Fact]
    public void Equals_EqualRepublics_ReturnsTrue()
    {
        Republic a = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);
        Republic b = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_DifferentCurrency_ReturnsFalse()
    {
        Republic a = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true)
        {
            Currency = new Currency("Доллар", "$")
        };
        Republic b = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true)
        {
            Currency = new Currency("Евро", "€")
        };

        Assert.False(a.Equals(b));
    }

    [Theory]
    [InlineData("Другой", 5, "Сенат", true)]
    [InlineData("Иванов", 4, "Сенат", true)]
    [InlineData("Иванов", 5, "Конгресс", true)]
    [InlineData("Иванов", 5, "Сенат", false)]
    public void Equals_DifferentExtraField_ReturnsFalse(
        string president, int term, string parliament, bool isPresidential)
    {
        Republic original = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);
        Republic other = new Republic("Страна", "Столица", 100u, 100.0,
            president, term, parliament, isPresidential);

        Assert.False(original.Equals(other));
    }

    [Theory]
    [InlineData("Другая", "Столица", 100u, 100.0)]
    [InlineData("Страна", "Другая", 100u, 100.0)]
    [InlineData("Страна", "Столица", 200u, 100.0)]
    [InlineData("Страна", "Столица", 100u, 200.0)]
    public void Equals_DifferentBaseField_ReturnsFalse(
        string name, string capital, uint population, double area)
    {
        Republic original = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);
        Republic other = new Republic(name, capital, population, area,
            "Иванов", 5, "Сенат", true);

        Assert.False(original.Equals(other));
    }

    [Fact]
    public void Equals_DifferentRuntimeType_ReturnsFalse()
    {
        Republic republic = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);
        Polity polity = new Polity("Страна", "Столица", 100u, 100.0);

        Assert.False(republic.Equals(polity));
        Assert.False(polity.Equals(republic));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        Republic republic = new Republic();

        Assert.False(republic.Equals(null));
    }

    [Fact]
    public void Equals_NotPolity_ReturnsFalse()
    {
        Republic republic = new Republic();

        Assert.False(republic.Equals(new object()));
    }

    // GETHASHCODE

    [Fact]
    public void GetHashCode_EqualRepublics_SameHash()
    {
        Republic a = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);
        Republic b = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // TOSTRING

    [Fact]
    public void ToString_PresidentialRepublic_ContainsAllFields()
    {
        Republic republic = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true)
        {
            Currency = new Currency("Доллар", "$")
        };

        string result = republic.ToString();

        Assert.Contains("Страна", result);
        Assert.Contains("Иванов", result);
        Assert.Contains("Сенат", result);
        Assert.Contains("5", result);
        Assert.Contains("президентская", result);
        Assert.Contains("Доллар", result);
        Assert.Contains("$", result);
    }

    [Fact]
    public void ToString_ParliamentaryRepublic_ContainsParliamentaryType()
    {
        Republic republic = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", false);

        string result = republic.ToString();

        Assert.Contains("парламентская", result);
        Assert.DoesNotContain("президентская", result);
    }

    // RANDOMINIT

    [Fact]
    public void RandomInit_SetsValidInvariants()
    {
        Republic republic = new Republic();
        republic.RandomInit();

        Assert.False(string.IsNullOrWhiteSpace(republic.Name));
        Assert.False(string.IsNullOrWhiteSpace(republic.Capital));
        Assert.True(republic.Population > 0);
        Assert.True(double.IsFinite(republic.Area) && republic.Area > 0);
        Assert.False(string.IsNullOrWhiteSpace(republic.President));
        Assert.InRange(republic.PresidentTermYears, 1, 10);
        Assert.False(string.IsNullOrWhiteSpace(republic.ParliamentName));

        Assert.NotNull(republic.Currency);
        Assert.NotEqual("Не задано", republic.Currency.Name);
    }
}