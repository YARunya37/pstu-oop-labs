using IOUtils;
using Polities;
namespace Program;

public class Organization : IInit
{
    public string Name
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Название организации не может быть пустым.");
        }
    }

    public uint MemberCount
    {
        get { return field; }
        set
        {
            if (value > 0)
                field = value;
            else
                throw new ArgumentException("Число участников должно быть не меньше одного.");
        }
    }

    protected static readonly Random Rnd = new();

    protected static readonly string[] SampleNames =
    [
        "ООН", "НАТО", "ВТО", "ЮНЕСКО", "Красный Крест", "Интерпол", "МАГАТЭ"
    ];

    public Organization()
    {
        Name = "Не задано";
        MemberCount = 1;
    }

    public Organization(string name, uint memberCount)
    {
        Name = name;
        MemberCount = memberCount;
    }

    // IInit

    public void Init()
    {
        Name = ReadString.ReadStringValue(
            "Введите название организации: ",
            "Название организации не может быть пустым.");

        MemberCount = ReadString.ReadPositiveNumber<uint>(
            "Введите число участников: ");
    }

    public void RandomInit()
    {
        Name = SampleNames[Rnd.Next(SampleNames.Length)];
        MemberCount = (uint)Rnd.Next(1, 201);
    }

    public override string ToString()
    {
        return $"Организация: {Name}, участников: {MemberCount}";
    }
}