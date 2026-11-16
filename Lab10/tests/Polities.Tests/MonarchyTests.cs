namespace Polities.Tests;

public class MonarchyTests
{
    // КОНСТРУКТОРЫ

    [Fact]
    public void Constructor_NoParams_SetsDefaultValues()
    {
        Monarchy monarchy = new Monarchy();

        Assert.Equal("Не задано", monarchy.Name);
        Assert.Equal("Не задано", monarchy.Capital);
        Assert.Equal(1u, monarchy.Population);
        Assert.Equal(1.0, monarchy.Area);
        Assert.Equal("Не задано", monarchy.Monarch);
        Assert.Equal("Не задано", monarchy.Title);
        Assert.Equal("Не задано", monarchy.Dynasty);
        Assert.False(monarchy.IsHereditary);
    }

    [Fact]
    public void Constructor_BaseOnly_SetsMonarchyDefaults()
    {
        Monarchy monarchy = new Monarchy("Страна", "Столица", 100u, 100.0);

        Assert.Equal("Страна", monarchy.Name);
        Assert.Equal("Не задано", monarchy.Monarch);
        Assert.Equal("Не задано", monarchy.Title);
        Assert.Equal("Не задано", monarchy.Dynasty);
        Assert.False(monarchy.IsHereditary);
    }

    [Fact]
    public void Constructor_Full_SetsAllValues()
    {
        Monarchy monarchy = new Monarchy(
            "Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.Equal("Страна", monarchy.Name);
        Assert.Equal("Столица", monarchy.Capital);
        Assert.Equal(100u, monarchy.Population);
        Assert.Equal(100.0, monarchy.Area);
        Assert.Equal("Александр", monarchy.Monarch);
        Assert.Equal("Король", monarchy.Title);
        Assert.Equal("Романовы", monarchy.Dynasty);
        Assert.True(monarchy.IsHereditary);
    }

    [Theory]
    [InlineData("", "Столица", 100u, 100.0, "Александр", "Король", "Романовы", true)]
    [InlineData("Страна", "", 100u, 100.0, "Александр", "Король", "Романовы", true)]
    [InlineData("Страна", "Столица", 0u, 100.0, "Александр", "Король", "Романовы", true)]
    [InlineData("Страна", "Столица", 100u, 0.0, "Александр", "Король", "Романовы", true)]
    [InlineData("Страна", "Столица", 100u, 100.0, "", "Король", "Романовы", true)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "", "Романовы", true)]
    [InlineData("Страна", "Столица", 100u, 100.0, "Александр", "Король", "", true)]
    public void Constructor_InvalidArgument_ThrowsArgumentException(
        string name, string capital, uint population, double area,
        string monarch, string title, string dynasty, bool isHereditary)
    {
        ArgumentException? caught = null;
        try
        {
            Monarchy monarchy = new Monarchy(
                name, capital, population, area,
                monarch, title, dynasty, isHereditary);
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
        Monarchy other = new Monarchy(
            "Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Monarchy copy = new Monarchy(other);

        Assert.Equal("Страна", copy.Name);
        Assert.Equal("Столица", copy.Capital);
        Assert.Equal(100u, copy.Population);
        Assert.Equal(100.0, copy.Area);
        Assert.Equal("Александр", copy.Monarch);
        Assert.Equal("Король", copy.Title);
        Assert.Equal("Романовы", copy.Dynasty);
        Assert.True(copy.IsHereditary);
    }

    [Fact]
    public void CopyConstructor_Null_ThrowsArgumentNullException()
    {
        ArgumentNullException? caught = null;
        try
        {
            Monarchy monarchy = new Monarchy((Monarchy)null!);
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
    public void Monarch_InvalidValue_ThrowsArgumentException(string? monarch)
    {
        Monarchy monarchy = new Monarchy();

        ArgumentException? caught = null;
        try
        {
            monarchy.Monarch = monarch!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Имя монарха не может быть пустым.", caught.Message);
    }

    [Fact]
    public void Monarch_ValidValueWithSpaces_TrimsValue()
    {
        Monarchy monarchy = new Monarchy();
        monarchy.Monarch = "  Александр  ";

        Assert.Equal("Александр", monarchy.Monarch);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Title_InvalidValue_ThrowsArgumentException(string? title)
    {
        Monarchy monarchy = new Monarchy();

        ArgumentException? caught = null;
        try
        {
            monarchy.Title = title!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Титул монарха не может быть пустым.", caught.Message);
    }

    [Fact]
    public void Title_ValidValueWithSpaces_TrimsValue()
    {
        Monarchy monarchy = new Monarchy();
        monarchy.Title = "  Король  ";

        Assert.Equal("Король", monarchy.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Dynasty_InvalidValue_ThrowsArgumentException(string? dynasty)
    {
        Monarchy monarchy = new Monarchy();

        ArgumentException? caught = null;
        try
        {
            monarchy.Dynasty = dynasty!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Название династии не может быть пустым.", caught.Message);
    }

    [Fact]
    public void Dynasty_ValidValueWithSpaces_TrimsValue()
    {
        Monarchy monarchy = new Monarchy();
        monarchy.Dynasty = "  Романовы  ";

        Assert.Equal("Романовы", monarchy.Dynasty);
    }

    // ДОМЕННЫЕ МЕТОДЫ

    [Fact]
    public void GetHeadOfState_ReturnsMonarch()
    {
        Monarchy monarchy = new Monarchy(
            "Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.Equal("Александр", monarchy.GetHeadOfState());
    }

    [Fact]
    public void GetFullTitle_ReturnsFormattedTitle()
    {
        Monarchy monarchy = new Monarchy(
            "Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.Equal("Король Александр (Романовы)", monarchy.GetFullTitle());
    }

    // EQUALS (через EqualsExtra)

    [Fact]
    public void Equals_SameObject_ReturnsTrue()
    {
        Monarchy monarchy = new Monarchy();

        Assert.True(monarchy.Equals(monarchy));
    }

    [Fact]
    public void Equals_EqualMonarchies_ReturnsTrue()
    {
        Monarchy a = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);
        Monarchy b = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.True(a.Equals(b));
    }

    [Theory]
    [InlineData("Николай", "Король", "Романовы", true)]      // Monarch
    [InlineData("Александр", "Император", "Романовы", true)] // Title
    [InlineData("Александр", "Король", "Тюдоры", true)]      // Dynasty
    [InlineData("Александр", "Король", "Романовы", false)]   // IsHereditary
    public void Equals_DifferentExtraField_ReturnsFalse(
        string monarch, string title, string dynasty, bool isHereditary)
    {
        Monarchy original = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);
        Monarchy other = new Monarchy("Страна", "Столица", 100u, 100.0,
            monarch, title, dynasty, isHereditary);

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
        Monarchy original = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);
        Monarchy other = new Monarchy(name, capital, population, area,
            "Александр", "Король", "Романовы", true);

        Assert.False(original.Equals(other));
    }

    [Fact]
    public void Equals_DifferentRuntimeType_Polity_ReturnsFalse()
    {
        Monarchy monarchy = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);
        Polity polity = new Polity("Страна", "Столица", 100u, 100.0);

        Assert.False(monarchy.Equals(polity));
        Assert.False(polity.Equals(monarchy));
    }

    [Fact]
    public void Equals_DifferentRuntimeType_Republic_ReturnsFalse()
    {
        Monarchy monarchy = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);
        Republic republic = new Republic("Страна", "Столица", 100u, 100.0,
            "Иванов", 5, "Сенат", true);

        Assert.False(monarchy.Equals(republic));
        Assert.False(republic.Equals(monarchy));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        Monarchy monarchy = new Monarchy();

        Assert.False(monarchy.Equals(null));
    }

    [Fact]
    public void Equals_NotPolity_ReturnsFalse()
    {
        Monarchy monarchy = new Monarchy();

        Assert.False(monarchy.Equals(new object()));
    }

    // GETHASHCODE (через GetHashCodeExtra)

    [Fact]
    public void GetHashCode_EqualMonarchies_SameHash()
    {
        Monarchy a = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);
        Monarchy b = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    // TOSTRING (через ToStringExtra)

    [Fact]
    public void ToString_HereditaryMonarchy_ContainsAllFields()
    {
        Monarchy monarchy = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", true);

        string result = monarchy.ToString();

        Assert.Contains("Страна", result);
        Assert.Contains("Александр", result);
        Assert.Contains("Король", result);
        Assert.Contains("Романовы", result);
        Assert.Contains("наследственная", result);
    }

    [Fact]
    public void ToString_ElectiveMonarchy_ContainsElectiveType()
    {
        Monarchy monarchy = new Monarchy("Страна", "Столица", 100u, 100.0,
            "Александр", "Король", "Романовы", false);

        string result = monarchy.ToString();

        Assert.Contains("выборная", result);
        Assert.DoesNotContain("наследственная", result);
    }

    // RANDOMINIT (через RandomInitExtra)

    [Fact]
    public void RandomInit_SetsValidInvariants()
    {
        Monarchy monarchy = new Monarchy();
        monarchy.RandomInit();

        Assert.False(string.IsNullOrWhiteSpace(monarchy.Name));
        Assert.True(monarchy.Population > 0);
        Assert.True(double.IsFinite(monarchy.Area) && monarchy.Area > 0);
        Assert.False(string.IsNullOrWhiteSpace(monarchy.Monarch));
        Assert.False(string.IsNullOrWhiteSpace(monarchy.Title));
        Assert.False(string.IsNullOrWhiteSpace(monarchy.Dynasty));
    }
}