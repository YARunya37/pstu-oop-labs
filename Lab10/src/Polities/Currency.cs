namespace Polities;

public class Currency
{
    public string Name
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Название валюты не может быть пустым.");
        }
    }

    public string Symbol
    {
        get { return field; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                field = value.Trim();
            else
                throw new ArgumentException("Символ валюты не может быть пустым.");
        }
    }

    public Currency()
    {
        Name = "Не задано";
        Symbol = "?";
    }

    public Currency(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    // Копирующий конструктор — для глубокого Clone() в Polity
    public Currency(Currency other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        Name = other.Name;
        Symbol = other.Symbol;
    }

    public override string ToString()
    {
        return $"{Name} ({Symbol})";
    }
}