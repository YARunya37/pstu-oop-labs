using IOUtils;

namespace Polities;

public class Republic : Polity
{
    // СВОЙСТВА

    public string President
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value;
            else
                throw new ArgumentException("Имя президента не может быть пустым.");
        }
    }

    public int PresidentTermYears
    {
        get { return field; }
        set
        {
            if (value >= 1 && value <= 10)
                field = value;
            else
                throw new ArgumentException("Срок президентских полномочий должен быть от 1 до 10 лет.");
        }
    }

    public string ParliamentName
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value;
            else
                throw new ArgumentException("Название парламента не может быть пустым.");
        }
    }

    public bool IsPresidential { get; set; }

    // ДАННЫЕ ДЛЯ ГЕНЕРАЦИИ

    protected static readonly string[] SampleFirstNames =
    [
        "Иван", "Пётр", "Сергей", "Алексей", "Дмитрий",
        "Андрей", "Михаил", "Николай", "Владимир", "Александр"
    ];

    protected static readonly string[] SampleLastNames =
    [
        "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов",
        "Волков", "Морозов", "Новиков", "Фёдоров", "Егоров"
    ];

    protected static readonly string[] SampleParliaments =
    [
        "Национальное собрание", "Конгресс", "Сенат", "Парламент",
        "Государственная дума", "Народное собрание"
    ];

    // КОНСТРУКТОРЫ 

    public Republic()
        : this("Не задано", "Не задано", 1u, 1.0, "Не задано", 1, "Не задано", false)
    {
    }

    public Republic(string name, string capital, uint population, double area)
        : this(name, capital, population, area, "Не задано", 1, "Не задано", false)
    {
    }

    public Republic(string name, string capital, uint population, double area,
        string president, int presidentTermYears, string parliamentName, bool isPresidential)
        : base(name, capital, population, area)
    {
        President = president;
        PresidentTermYears = presidentTermYears;
        ParliamentName = parliamentName;
        IsPresidential = isPresidential;
    }

    public Republic(Republic other) : base(other)
    {
        President = other.President;
        PresidentTermYears = other.PresidentTermYears;
        ParliamentName = other.ParliamentName;
        IsPresidential = other.IsPresidential;
    }

    // МЕТОДЫ ДЛЯ ЗАПРОСОВ
    public override string GetHeadOfState()
    {
        return President;
    }

    public bool IsParliamentNamed(string name)
    {
        return string.Equals(ParliamentName, name?.Trim(), StringComparison.OrdinalIgnoreCase);
    } 

    // ИНИЦИАЛИЗАЦИЯ

    protected override void InitExtra()
    {
        President = ReadString.ReadStringValue(
            "Введите имя президента: ",
            "Имя президента не может быть пустым.");

        PresidentTermYears = ReadString.ReadPositiveNumber<int>(
            "Введите срок полномочий президента (от 1 до 10 лет): ");

        ParliamentName = ReadString.ReadStringValue(
            "Введите название парламента: ",
            "Название парламента не может быть пустым.");

        // Считываем выбор пользователя: 1 или 0, которые приводятся к bool
        IsPresidential = Convert.ToBoolean(ReadString.ReadIntChoice("Республика президентская? (1 — да, 0 — нет):\nВведите 1 или 0.",
         "Ошибка: введите 1 или 0.",
            1, 0
         ));
    }

    protected override void RandomInitExtra()
    {
        // Polity.RandomInitExtra() пустой — base-вызов не нужен
        string firstName = SampleFirstNames[Rnd.Next(SampleFirstNames.Length)];
        string lastName = SampleLastNames[Rnd.Next(SampleLastNames.Length)];
        President = $"{firstName} {lastName}";
        PresidentTermYears = Rnd.Next(4, 8);                  // 4..7
        ParliamentName = SampleParliaments[Rnd.Next(SampleParliaments.Length)];
        IsPresidential = Rnd.Next(2) == 1;
    }

    // ВЫВОД

    protected override string ToStringExtra()
    {
        // Polity.ToStringExtra() возвращает "" — base-вызов не нужен
        string type = IsPresidential ? "президентская" : "парламентская";
        return $"Республика ({type}). Президент: {President}, срок: {PresidentTermYears} лет. " +
               $"Парламент: {ParliamentName}";
    }

    // СРАВНЕНИЕ

    protected override bool EqualsExtra(Polity other)
    {
        // GetType() уже проверен в Polity.Equals, каст безопасен
        Republic republic = (Republic)other;
        return President == republic.President &&
            PresidentTermYears == republic.PresidentTermYears &&
            ParliamentName == republic.ParliamentName &&
            IsPresidential == republic.IsPresidential;
    }

    protected override int GetHashCodeExtra()
    {
        return HashCode.Combine(
            President,
            PresidentTermYears,
            ParliamentName,
            IsPresidential);
    }
}