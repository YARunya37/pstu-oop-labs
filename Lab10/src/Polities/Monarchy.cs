using IOUtils;

namespace Polities;

public class Monarchy : Polity
{
    // СВОЙСТВА

    public string Monarch
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Имя монарха не может быть пустым.");
        }
    }

    public string Title
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Титул монарха не может быть пустым.");
        }
    }

    public string Dynasty
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Название династии не может быть пустым.");
        }
    }

    public bool IsHereditary { get; set; }

    // ДАННЫЕ ДЛЯ ГЕНЕРАЦИИ

    protected static readonly string[] SampleMonarchNames =
    [
        "Александр", "Николай", "Пётр", "Иван", "Михаил",
        "Фёдор", "Борис", "Василий", "Дмитрий", "Ярослав"
    ];

    protected static readonly string[] SampleTitles =
    [
        "Король", "Император", "Султан", "Царь", "Хан", "Шах"
    ];

    protected static readonly string[] SampleDynasties =
    [
        "Романовы", "Тюдоры", "Бурбоны", "Габсбурги",
        "Валуа", "Плантагенеты", "Стюарты", "Медичи"
    ];

    // КОНСТРУКТОРЫ (перегрузка)

    public Monarchy()
        : this("Не задано", "Не задано", 1u, 1.0, "Не задано", "Не задано", "Не задано", false)
    {
    }

    public Monarchy(string name, string capital, uint population, double area)
        : this(name, capital, population, area, "Не задано", "Не задано", "Не задано", false)
    {
    }

    public Monarchy(string name, string capital, uint population, double area,
        string monarch, string title, string dynasty, bool isHereditary)
        : base(name, capital, population, area)
    {
        Monarch = monarch;
        Title = title;
        Dynasty = dynasty;
        IsHereditary = isHereditary;
    }

    public Monarchy(Monarchy other) : base(other)
    {
        Monarch = other.Monarch;
        Title = other.Title;
        Dynasty = other.Dynasty;
        IsHereditary = other.IsHereditary;
    }

    // МЕТОДЫ ДЛЯ ЗАПРОСОВ

    // Глава государства — монарх
    public override string GetHeadOfState()
    {
        return Monarch;
    }
    public override string GetGovernmentType()
    {
        return IsHereditary ? "наследственная монархия" : "выборная монархия";
    }

    // Полный титул: "Король Александр (Романовы)"
    public string GetFullTitle()
    {
        return $"{Title} {Monarch} ({Dynasty})";
    }

    // ИНИЦИАЛИЗАЦИЯ

    protected override void InitExtra()
    {
        Monarch = ReadString.ReadStringValue(
            "Введите имя монарха: ",
            "Имя монарха не может быть пустым.");

        Title = ReadString.ReadStringValue(
            "Введите титул монарха: ",
            "Титул монарха не может быть пустым.");

        Dynasty = ReadString.ReadStringValue(
            "Введите название династии: ",
            "Название династии не может быть пустым.");

        IsHereditary = Convert.ToBoolean(ReadString.ReadIntChoice(
            "Монархия наследственная? (1 — да, 0 — нет):\nВведите 1 или 0.",
            "Ошибка: введите 1 или 0.",
            1, 0));
    }

    protected override void RandomInitExtra()
    {
        Monarch = SampleMonarchNames[Rnd.Next(SampleMonarchNames.Length)];
        Title = SampleTitles[Rnd.Next(SampleTitles.Length)];
        Dynasty = SampleDynasties[Rnd.Next(SampleDynasties.Length)];
        IsHereditary = Rnd.Next(2) == 1;
    }

    // ВЫВОД


    // Hiding
    public new void ShowRegularInfo()
    {
        base.ShowRegularInfo();   // Polity.ShowRegularInfo
        Console.WriteLine(
            $"[Regular] Монархия ({GetGovernmentType()}). {Title}: {Monarch}, " +
            $"династия: {Dynasty}");
    }

    // Override
    public override void ShowVirtualInfo()
    {
        base.ShowVirtualInfo();   // Polity.ShowVirtualInfo
        Console.WriteLine(
            $"[Virtual] Монархия ({GetGovernmentType()}). {Title}: {Monarch}, " +
            $"династия: {Dynasty}");
    }

    protected override string ToStringExtra()
    {
        string type = IsHereditary ? "наследственная" : "выборная";
        return $"Монархия ({type}). {Title}: {Monarch}, династия: {Dynasty}";
    }

    // СРАВНЕНИЕ

    protected override bool EqualsExtra(Polity other)
    {
        Monarchy monarchy = (Monarchy)other;
        return Monarch == monarchy.Monarch &&
            Title == monarchy.Title &&
            Dynasty == monarchy.Dynasty &&
            IsHereditary == monarchy.IsHereditary;
    }

    protected override int GetHashCodeExtra()
    {
        return HashCode.Combine(
            Monarch,
            Title,
            Dynasty,
            IsHereditary);
    }
}