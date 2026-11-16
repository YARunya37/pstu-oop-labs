using IOUtils;

namespace Polities;

public class Kingdom : Monarchy
{
    // СВОЙСТВА

    public string RoyalResidence
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Название резиденции не может быть пустым.");
        }
    }

    public uint DuchiesCount
    {
        get { return field; }
        set
        {
            if (value > 0)
                field = value;
            else
                throw new ArgumentException("Количество герцогств должно быть не меньше одного.");
        }
    }

    public double CrownJewelsValue
    {
        get { return field; }
        set
        {
            if (double.IsFinite(value) && value > 0)
                field = value;
            else
                throw new ArgumentException("Стоимость регалий должна быть конечным положительным числом.");
        }
    }

    // ДАННЫЕ ДЛЯ ГЕНЕРАЦИИ

    protected static readonly string[] SampleResidences =
    [
        "Вестминстерский дворец", "Версаль", "Зимний дворец",
        "Кремль", "Букингемский дворец", "Лувр", "Топкапы", "Альгамбра"
    ];

    // КОНСТРУКТОРЫ (перегрузка)

    public Kingdom()
        : this("Не задано", "Не задано", 1u, 1.0,
               "Не задано", "Не задано", "Не задано", false,
               "Не задано", 1u, 1.0)
    {
    }

    public Kingdom(string name, string capital, uint population, double area)
        : this(name, capital, population, area,
               "Не задано", "Не задано", "Не задано", false,
               "Не задано", 1u, 1.0)
    {
    }

    public Kingdom(string name, string capital, uint population, double area,
        string monarch, string title, string dynasty, bool isHereditary)
        : this(name, capital, population, area,
               monarch, title, dynasty, isHereditary,
               "Не задано", 1u, 1.0)
    {
    }

    public Kingdom(string name, string capital, uint population, double area,
        string monarch, string title, string dynasty, bool isHereditary,
        string royalResidence, uint duchiesCount, double crownJewelsValue)
        : base(name, capital, population, area, monarch, title, dynasty, isHereditary)
    {
        RoyalResidence = royalResidence;
        DuchiesCount = duchiesCount;
        CrownJewelsValue = crownJewelsValue;
    }

    public Kingdom(Kingdom other) : base(other)
    {
        RoyalResidence = other.RoyalResidence;
        DuchiesCount = other.DuchiesCount;
        CrownJewelsValue = other.CrownJewelsValue;
    }

    // МЕТОДЫ ДЛЯ ЗАПРОСОВ

    // GetHeadOfState НЕ переопределяем: Monarchy.GetHeadOfState() уже возвращает Monarch,
    // а у Kingdom поле Monarch унаследовано от Monarchy. Наследование работает как надо.

    // Совпадает ли резиденция (без учёта регистра)
    public bool HasRoyalResidence(string name)
    {
        return string.Equals(RoyalResidence, name?.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    // Стоимость регалий превышает порог
    public bool IsWealthierThan(double threshold)
    {
        return CrownJewelsValue > threshold;
    }

    // ИНИЦИАЛИЗАЦИЯ

    protected override void InitExtra()
    {
        base.InitExtra();   // Monarchy: Monarch, Title, Dynasty, IsHereditary

        RoyalResidence = ReadString.ReadStringValue(
            "Введите название резиденции: ",
            "Название резиденции не может быть пустым.");

        DuchiesCount = ReadString.ReadPositiveNumber<uint>(
            "Введите количество герцогств: ");

        CrownJewelsValue = ReadString.ReadPositiveNumber<double>(
            "Введите стоимость королевских регалий: ");
    }

    protected override void RandomInitExtra()
    {
        base.RandomInitExtra();   // Monarchy: Monarch, Title, Dynasty, IsHereditary

        RoyalResidence = SampleResidences[Rnd.Next(SampleResidences.Length)];
        DuchiesCount = (uint)Rnd.Next(1, 21);                                  // 1..20
        CrownJewelsValue = Math.Round(Rnd.NextDouble() * (1_000_000 - 1_000) + 1_000, 2);
    }

    // ВЫВОД

    // Hiding: цепочка Kingdom => Monarchy => Polity
    public new void ShowRegularInfo()
    {
        base.ShowRegularInfo();   // Monarchy.ShowRegularInfo (new), а он вызывает Polity
        Console.WriteLine(
            $"[Regular] Королевство. Резиденция: {RoyalResidence}, " +
            $"герцогств: {DuchiesCount}, регалии: {CrownJewelsValue:N2}");
    }

    // Override: цепочка Kingdom => Monarchy => Polity
    public override void ShowVirtualInfo()
    {
        base.ShowVirtualInfo();   // Monarchy.ShowVirtualInfo (override), а он вызывает Polity
        Console.WriteLine(
            $"[Virtual] Королевство. Резиденция: {RoyalResidence}, " +
            $"герцогств: {DuchiesCount}, регалии: {CrownJewelsValue:N2}");
    }

    protected override string ToStringExtra()
    {
        string kingdomInfo =
            $"Резиденция: {RoyalResidence}, герцогств: {DuchiesCount}, " +
            $"регалии: {CrownJewelsValue:N2}";
        return $"{base.ToStringExtra()}\n{kingdomInfo}";
    }

    // СРАВНЕНИЕ

    protected override bool EqualsExtra(Polity other)
    {
        Kingdom kingdom = (Kingdom)other;

        // base.EqualsExtra(other) — Monarchy.EqualsExtra, сравнивает Monarch/Title/Dynasty/IsHereditary.
        return base.EqualsExtra(other) &&
            RoyalResidence == kingdom.RoyalResidence &&
            DuchiesCount == kingdom.DuchiesCount &&
            CrownJewelsValue == kingdom.CrownJewelsValue;
    }

    protected override int GetHashCodeExtra()
    {
        return HashCode.Combine(
            base.GetHashCodeExtra(),
            RoyalResidence,
            DuchiesCount,
            CrownJewelsValue);
    }
}