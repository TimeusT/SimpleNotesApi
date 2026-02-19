using System.Text.RegularExpressions;

namespace SimpleNotes.Domain;

public class AlphaText
{
    public static readonly AlphaText Null = new AlphaText(string.Empty);

    public string Value { get; }

    public AlphaText(string value)
    {
        if (!Regex.IsMatch(value, "^[A-Za-z ]+$"))
        {
            throw new ArgumentException("Value must only be letters and white space.");
        }

        Value = value;
    }

    public bool IsNull => this == Null;

    public static AlphaText Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Null;
        }
        return new AlphaText(value);
    }
}
