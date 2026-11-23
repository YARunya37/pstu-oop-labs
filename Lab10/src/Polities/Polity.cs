using IOUtils;

namespace Polities;

public class Polity : IInit, IComparable
{
    public string Name
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                field = value;
            }
            else
            {
                throw new ArgumentException("Название государства не может быть пустым.");
            }
        }
    }

    public string Capital
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                field = value;
            }
            else
            {
                throw new ArgumentException("Название столицы не может быть пустым.");
            }
        }
    }

    public uint Population
    {
        get { return field; }
        set
        {
            if (value > 0)
            {
                field = value;
            }
            else
            {
                throw new ArgumentException("Численность населения не может быть меньше хотя бы одного человека");
            }
        }
    }

    public double Area
    {
        get { return field; }
        set
        {
            if (double.IsFinite(value) && value > 0)
            {
                field = value;
            }
            else
            {
                throw new ArgumentException("Площадь должна быть конечным положительным числом");
            }
        }
    }

    // Общий генератор для базового класса и всех наследников
    protected static readonly Random Rnd = new();

    // Словари для генерации
    protected static readonly string[] SampleNames =
    [
        "Арстотцка", "Колечия", "Орбистан", "Антегрия", "Республия",
            "Импор", "Федерация", "Атлантида", "Эльдорадо", "Вальгалла"
    ];

    protected static readonly string[] SampleCapitals =
    [
        "Парадиз", "Вест-Грестин", "Ист-Грестин", "Орбис", "Сент-Мареро",
            "Тру-Грестин", "Нова-Град", "Аурум", "Асгард", "Полис"
    ];


    // КОНСТРУКТОРЫ

    public Polity()
    {
        Name = "Не задано";
        Capital = "Не задано";
        Population = 1;
        Area = 1;
    }

    public Polity(string name, string capital, uint population, double area)
    {
        Name = name;
        Capital = capital;
        Population = population;
        Area = area;
    }

    public Polity(Polity other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));


        Name = other.Name;
        Capital = other.Capital;
        Population = other.Population;
        Area = other.Area;
    }


    public virtual string GetHeadOfState() => "Без системы правления нет главы государства";

    public virtual string GetGovernmentType()
    {
        return "Без системы правления";
    }

    // ИНИЦИАЛИЗАЦИЯ

    // ДСЧ
    public void RandomInit()
    {
        // Выбираем случайную страну и добавляем суффикс, чтобы имена реже повторялись
        string baseName = SampleNames[Rnd.Next(SampleNames.Length)];
        Name = $"{baseName}-{Rnd.Next(1, 100)}";

        Capital = SampleCapitals[Rnd.Next(SampleCapitals.Length)];

        // Население от 100 тысяч до 150 миллионов
        // (Для uint удобно использовать NextInt64 и привести к uint)
        Population = (uint)Rnd.NextInt64(100_000, 150_000_000);

        // Площадь от 1 000 до 10 000 000 км² с округлением до 2 знаков
        double rawArea = Rnd.NextDouble() * (10_000_000 - 1_000) + 1_000;
        Area = Math.Round(rawArea, 2);

        RandomInitExtra();
    }

    // При переопределении вызывать base.*Extra()
    protected virtual void RandomInitExtra() { }

    // Ввод с клавиатуры
    public void Init()
    {
        InitName();
        InitCapital();
        InitPopulation();
        InitArea();
        InitExtra();
    }
    void InitName()
    {
        Name = ReadString.ReadStringValue("Введите название государства: ", "Название государства не может быть пустым.");
    }
    void InitCapital()
    {
        Capital = ReadString.ReadStringValue("Введите название столицы: ", "Название столицы не может быть пустым.");
    }
    void InitPopulation()
    {
        Population = ReadString.ReadPositiveNumber<uint>("Введите численность населения: ");
    }
    void InitArea()
    {
        Area = ReadString.ReadPositiveNumber<double>("Введите площадь территории государства: ");
    }

    // При переопределении вызывать base.*Extra()
    protected virtual void InitExtra() { }

    // Вывод 

    public void Show()
    {
        Console.WriteLine($"{ToString()}");
    }

    // ДЕМОНСТРАЦИЯ ВЫВОДА ПО ЗАДАНИЮ

    // Обычная функция. В наследниках скрывается через new.
    public void ShowRegularInfo()
    {
        Console.WriteLine(
            $"[Regular] Государство: {Name}, Столица: {Capital}, " +
            $"Население: {Population:N0}, Площадь: {Area:N2}");
    }

    // Виртуальная функция. В наследниках переопределяется через override.
    public virtual void ShowVirtualInfo()
    {
        Console.WriteLine(
            $"[Virtual] Государство: {Name}, Столица: {Capital}, " +
            $"Население: {Population:N0}, Площадь: {Area:N2}");
    }

    // ICOMPARABLE

    public int CompareTo(object? obj)
    {
        // Любой объект больше null
        if(obj == null)
            return 1;

        // Если не Polity, то не можем сравнить
        if(obj is not Polity other)
            throw new ArgumentException();

        return Name.CompareTo(other.Name);
    }

    // Методы object.


    // При переопределении вызывать base.*Extra()
    public sealed override string ToString()
    {
        // Формат :N0 добавит разделители разрядов для населения (например, 12 500 000), 
        // а :N2 выведет площадь с двумя знаками после запятой.
        string result = $"Государство: {Name}, Столица: {Capital}, Население: {Population:N0} чел., Площадь: {Area:N2} км²";

        string extra = ToStringExtra();
        if (!string.IsNullOrWhiteSpace(extra))
        {
            result += $"\n{extra}";
        }

        return result;
    }

    protected virtual string ToStringExtra() => "";

    // override object.Equals

    // При переопределении EqualsExtra вызывать base.EqualsExtra() и переопределять GetHashCodeExtra (и наоборот)

    public sealed override bool Equals(object? obj)
    {
        // Если в классе в будущем будет перегружен оператор ==, запись this == obj может привести к бесконечной рекурсии
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is not Polity other || GetType() != obj.GetType())
        {
            return false;
        }


        return Name == other.Name &&
            Capital == other.Capital &&
            Population == other.Population &&
            Area == other.Area &&
            EqualsExtra(other);
    }

    protected virtual bool EqualsExtra(Polity other) => true;

    // override object.GetHashCode
    public sealed override int GetHashCode()
    {
        return HashCode.Combine(Name, Capital, Population, Area, GetHashCodeExtra());
    }

    protected virtual int GetHashCodeExtra() => 0;
}
