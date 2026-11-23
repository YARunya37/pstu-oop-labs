namespace Polities.Tests;

public class KingdomTests
{
    // КОНСТРУКТОРЫ

    [Fact]
    public void Constructor_NoParams_SetsDefaultValues()
    {
        Kingdom kingdom = new Kingdom();

        Assert.Equal("Не задано", kingdom.Name);
        Assert.Equal("Не задано", kingdom.Capital);
        Assert.Equal(1u, kingdom.Population);
        Assert.Equal(1.0, kingdom.Area);
        Assert.Equal("Не задано", kingdom.Monarch);
        Assert.Equal("Не задано", kingdom.Title);
        Assert.Equal("Не задано", kingdom.Dynasty);
        Assert.False(kingdom.IsHereditary);
        Assert.Equal("Не задано", kingdom.RoyalResidence);
        Assert.Equal(1u, kingdom.DuchiesCount);
        Assert.Equal(1.0, kingdom.CrownJewelsValue);

        Assert.NotNull(kingdom.Currency);
        Assert.Equal("Не задано", kingdom.Currency.Name);
        Assert.Equal("?", kingdom.Currency.Symbol);
    }

    [Fact]
    public void Constructor_BaseOnly_SetsKingdomDefaults()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0);

        Assert.Equal("Страна", kingdom.Name);
        Assert.Equal("Не задано", kingdom.Monarch);
        Assert.Equal("Не задано", kingdom.RoyalResidence);
        Assert.Equal(1u, kingdom.DuchiesCount);
        Assert.NotNull(kingdom.Currency);
    }

    [Fact]
    public void Constructor_MonarchyLevel_SetsKingdomDefaults()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.Equal("Александр", kingdom.Monarch);
        Assert.Equal("Король", kingdom.Title);
        Assert.Equal("Романовы", kingdom.Dynasty);
        Assert.True(kingdom.IsHereditary);
        Assert.Equal("Не задано", kingdom.RoyalResidence);
        Assert.Equal(1u, kingdom.DuchiesCount);
        Assert.NotNull(kingdom.Currency);
    }

    [Fact]
    public void Constructor_Full_SetsAllValues()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.Equal("Страна", kingdom.Name);
        Assert.Equal("Столица", kingdom.Capital);
        Assert.Equal(100u, kingdom.Population);
        Assert.Equal(100.0, kingdom.Area);
        Assert.Equal("Александр", kingdom.Monarch);
        Assert.Equal("Король", kingdom.Title);
        Assert.Equal("Романовы", kingdom.Dynasty);
        Assert.True(kingdom.IsHereditary);
        Assert.Equal("Вестминстер", kingdom.RoyalResidence);
        Assert.Equal(5u, kingdom.DuchiesCount);
        Assert.Equal(1000.0, kingdom.CrownJewelsValue);

        Assert.NotNull(kingdom.Currency);
        Assert.Equal("Не задано", kingdom.Currency.Name);
    }

    [Theory]
    [InlineData("", "Столица", 100u, 100.0, "Александр", "Король", "Романовы", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "", 100u, 100.0, "Александр", "Король", "Романовы", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 0u, 100.0, "Александр", "Король", "Романовы", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 0.0, "Александр", "Король", "Романовы", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 100.0, "", "Король", "Романовы", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "", "Романовы", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "Король", "", true, "Вестминстер", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "Король", "Романовы", true, "", 5u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "Король", "Романовы", true, "Вестминстер", 0u, 1000.0)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "Король", "Романовы", true, "Вестминстер", 5u, 0.0)]
    public void Constructor_InvalidArgument_ThrowsArgumentException(
        string name, string capital, uint population, double area,
        string monarch, string title, string dynasty, bool isHereditary,
        string royalResidence, uint duchiesCount, double crownJewelsValue)
    {
        ArgumentException? caught = null;
        try
        {
            Kingdom kingdom = new Kingdom(name, capital, population, area,
                monarch, title, dynasty, isHereditary,
                royalResidence, duchiesCount, crownJewelsValue);
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
        Kingdom other = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0)
        {
            Currency = new Currency("Фунт", "£")
        };

        Kingdom copy = new Kingdom(other);

        Assert.Equal("Страна", copy.Name);
        Assert.Equal("Александр", copy.Monarch);
        Assert.Equal("Король", copy.Title);
        Assert.Equal("Романовы", copy.Dynasty);
        Assert.True(copy.IsHereditary);
        Assert.Equal("Вестминстер", copy.RoyalResidence);
        Assert.Equal(5u, copy.DuchiesCount);
        Assert.Equal(1000.0, copy.CrownJewelsValue);

        Assert.NotNull(copy.Currency);
        Assert.NotSame(other.Currency, copy.Currency);
        Assert.Equal("Фунт", copy.Currency.Name);
    }

    [Fact]
    public void CopyConstructor_Null_ThrowsArgumentNullException()
    {
        ArgumentNullException? caught = null;
        try
        {
            Kingdom kingdom = new Kingdom((Kingdom)null!);
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
    public void RoyalResidence_InvalidValue_ThrowsArgumentException(string? residence)
    {
        Kingdom kingdom = new Kingdom();

        ArgumentException? caught = null;
        try
        {
            kingdom.RoyalResidence = residence!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Название резиденции не может быть пустым.", caught.Message);
    }

    [Fact]
    public void RoyalResidence_ValidValueWithSpaces_TrimsValue()
    {
        Kingdom kingdom = new Kingdom();
        kingdom.RoyalResidence = "  Вестминстер  ";

        Assert.Equal("Вестминстер", kingdom.RoyalResidence);
    }

    [Theory]
    [InlineData(0u)]
    public void DuchiesCount_InvalidValue_ThrowsArgumentException(uint count)
    {
        Kingdom kingdom = new Kingdom();

        ArgumentException? caught = null;
        try
        {
            kingdom.DuchiesCount = count;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Количество герцогств должно быть не меньше одного.", caught.Message);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void CrownJewelsValue_InvalidValue_ThrowsArgumentException(double value)
    {
        Kingdom kingdom = new Kingdom();

        ArgumentException? caught = null;
        try
        {
            kingdom.CrownJewelsValue = value;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Стоимость регалий должна быть конечным положительным числом.", caught.Message);
    }

    // ДОМЕННЫЕ МЕТОДЫ

    [Fact]
    public void GetHeadOfState_InheritedFromMonarchy_ReturnsMonarch()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.Equal("Александр", kingdom.GetHeadOfState());
    }

    [Fact]
    public void GetFullTitle_InheritedFromMonarchy_ReturnsFormattedTitle()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.Equal("Король Александр (Романовы)", kingdom.GetFullTitle());
    }

    [Theory]
    [InlineData("Вестминстер", true)]
    [InlineData("вестминстер", true)]
    [InlineData("ВЕСТМИНСТЕР", true)]
    [InlineData("  Вестминстер  ", true)]
    [InlineData("Версаль", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void HasRoyalResidence_VariousInputs_ReturnsExpected(string? query, bool expected)
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.Equal(expected, kingdom.HasRoyalResidence(query!));
    }

    [Theory]
    [InlineData(500.0, true)]
    [InlineData(1000.0, false)]
    [InlineData(1500.0, false)]
    public void IsWealthierThan_VariousThresholds_ReturnsExpected(double threshold, bool expected)
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.Equal(expected, kingdom.IsWealthierThan(threshold));
    }

    // EQUALS

    [Fact]
    public void Equals_DifferentCurrency_ReturnsFalse()
    {
        Kingdom a = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0)
        {
            Currency = new Currency("Фунт", "£")
        };
        Kingdom b = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0)
        {
            Currency = new Currency("Евро", "€")
        };

        Assert.False(a.Equals(b));
    }

    [Theory]
    [InlineData("Николай", "Король", "Романовы", true)]
    [InlineData("Александр", "Император", "Романовы", true)]
    [InlineData("Александр", "Король", "Тюдоры", true)]
    [InlineData("Александр", "Король", "Романовы", false)]
    public void Equals_DifferentMonarchyField_ReturnsFalse(
        string monarch, string title, string dynasty, bool isHereditary)
    {
        Kingdom original = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Kingdom other = new Kingdom("Страна", "Столица", 100u, 100.0,
            monarch, title, dynasty, isHereditary,
            "Вестминстер", 5u, 1000.0);

        Assert.False(original.Equals(other));
    }

    [Theory]
    [InlineData("Версаль", 5u, 1000.0)]
    [InlineData("Вестминстер", 10u, 1000.0)]
    [InlineData("Вестминстер", 5u, 2000.0)]
    public void Equals_DifferentKingdomField_ReturnsFalse(
        string residence, uint duchies, double crownJewels)
    {
        Kingdom original = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Kingdom other = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            residence, duchies, crownJewels);

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
        Kingdom original = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Kingdom other = new Kingdom(name, capital, population, area,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.False(original.Equals(other));
    }

    [Fact]
    public void Equals_DifferentRuntimeType_Monarchy_ReturnsFalse()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Monarchy monarchy = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.False(kingdom.Equals(monarchy));
        Assert.False(monarchy.Equals(kingdom));
    }

    [Fact]
    public void Equals_DifferentRuntimeType_Polity_ReturnsFalse()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Polity polity = new Polity("Страна", "Столица", 100u, 100.0);

        Assert.False(kingdom.Equals(polity));
        Assert.False(polity.Equals(kingdom));
    }

    [Fact]
    public void Equals_SameObject_ReturnsTrue()
    {
        Kingdom kingdom = new Kingdom();

        Assert.True(kingdom.Equals(kingdom));
    }

    [Fact]
    public void Equals_EqualKingdoms_ReturnsTrue()
    {
        Kingdom a = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Kingdom b = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        Kingdom kingdom = new Kingdom();

        Assert.False(kingdom.Equals(null));
    }

    // GETHASHCODE

    [Fact]
    public void GetHashCode_EqualKingdoms_SameHash()
    {
        Kingdom a = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);
        Kingdom b = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // TOSTRING

    [Fact]
    public void ToString_ContainsBothMonarchyAndKingdomFields()
    {
        Kingdom kingdom = new Kingdom("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true,
            "Вестминстер", 5u, 1000.0)
        {
            Currency = new Currency("Фунт", "£")
        };

        string result = kingdom.ToString();

        Assert.Contains("Страна", result);
        Assert.Contains("Столица", result);
        Assert.Contains("Монархия", result);
        Assert.Contains("Александр", result);
        Assert.Contains("Король", result);
        Assert.Contains("Романовы", result);
        Assert.Contains("Вестминстер", result);
        Assert.Contains("5", result);
        Assert.Contains("герцогств", result);
        Assert.Contains("Фунт", result);
        Assert.Contains("£", result);
    }

    // RANDOMINIT

    [Fact]
    public void RandomInit_SetsAllInvariants_IncludingInherited()
    {
        Kingdom kingdom = new Kingdom();
        kingdom.RandomInit();

        Assert.False(string.IsNullOrWhiteSpace(kingdom.Name));
        Assert.NotEqual("Не задано", kingdom.Name);
        Assert.True(kingdom.Population > 0);
        Assert.True(double.IsFinite(kingdom.Area) && kingdom.Area > 0);

        Assert.False(string.IsNullOrWhiteSpace(kingdom.Monarch));
        Assert.NotEqual("Не задано", kingdom.Monarch);
        Assert.False(string.IsNullOrWhiteSpace(kingdom.Title));
        Assert.NotEqual("Не задано", kingdom.Title);
        Assert.False(string.IsNullOrWhiteSpace(kingdom.Dynasty));
        Assert.NotEqual("Не задано", kingdom.Dynasty);

        Assert.False(string.IsNullOrWhiteSpace(kingdom.RoyalResidence));
        Assert.NotEqual("Не задано", kingdom.RoyalResidence);
        Assert.True(kingdom.DuchiesCount > 0);
        Assert.True(double.IsFinite(kingdom.CrownJewelsValue) && kingdom.CrownJewelsValue > 0);

        Assert.NotNull(kingdom.Currency);
        Assert.NotEqual("Не задано", kingdom.Currency.Name);
    }
}