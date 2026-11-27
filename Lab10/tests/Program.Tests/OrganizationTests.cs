using Program;
using Polities;

namespace Program.Tests;

public class OrganizationTests
{
    // КОНСТРУКТОРЫ

    [Fact]
    public void Constructor_NoParams_SetsDefaultValues()
    {
        Organization org = new Organization();

        Assert.Equal("Не задано", org.Name);
        Assert.Equal(1u, org.MemberCount);
    }

    [Fact]
    public void Constructor_ValidValues_SetsAllValues()
    {
        Organization org = new Organization("ООН", 193u);

        Assert.Equal("ООН", org.Name);
        Assert.Equal(193u, org.MemberCount);
    }

    [Theory]
    [InlineData("", 1u)]
    [InlineData("    ", 1u)]
    [InlineData("ООН", 0u)]
    public void Constructor_InvalidArgument_ThrowsArgumentException(string name, uint memberCount)
    {
        ArgumentException? caught = null;
        try
        {
            Organization org = new Organization(name, memberCount);
        }
        catch (ArgumentException e)
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
        Organization org = new Organization();

        ArgumentException? caught = null;
        try
        {
            org.Name = name!;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Название организации не может быть пустым.", caught.Message);
    }

    [Fact]
    public void Name_ValidValueWithSpaces_TrimsValue()
    {
        Organization org = new Organization();
        org.Name = "  ООН  ";

        Assert.Equal("ООН", org.Name);
    }

    [Theory]
    [InlineData(0u)]
    public void MemberCount_InvalidValue_ThrowsArgumentException(uint count)
    {
        Organization org = new Organization();

        ArgumentException? caught = null;
        try
        {
            org.MemberCount = count;
        }
        catch (ArgumentException e)
        {
            caught = e;
        }

        Assert.NotNull(caught);
        Assert.Equal("Число участников должно быть не меньше одного.", caught.Message);
    }

    // IINIT

    [Fact]
    public void Organization_ImplementsIInit()
    {
        IInit org = new Organization();
        org.RandomInit();

        Organization concrete = (Organization)org;
        Assert.False(string.IsNullOrWhiteSpace(concrete.Name));
    }

    [Fact]
    public void RandomInit_SetsValidInvariants()
    {
        Organization org = new Organization();
        org.RandomInit();

        Assert.False(string.IsNullOrWhiteSpace(org.Name));
        Assert.NotEqual("Не задано", org.Name);
        Assert.True(org.MemberCount > 0);
        Assert.InRange(org.MemberCount, 1u, 200u);   // документированный диапазон RandomInit
    }

    // TOSTRING

    [Fact]
    public void ToString_ContainsNameAndMemberCount()
    {
        Organization org = new Organization("НАТО", 30u);

        string result = org.ToString();

        Assert.Contains("НАТО", result);
        Assert.Contains("30", result);
        Assert.Contains("Организация", result);
    }
}