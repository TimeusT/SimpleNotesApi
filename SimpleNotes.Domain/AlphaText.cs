using System.Text.RegularExpressions;

namespace SimpleNotes.Domain;

public class AlphaText
{
    public static readonly AlphaText Null = new AlphaText(string.Empty);

    public string Value { get; }

    private AlphaText(string value)
    {
        Value = value;
    }

    public bool IsNull => string.IsNullOrEmpty(Value);

    public static AlphaText Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Null;

        if (!Regex.IsMatch(value, "^[A-Za-z ]+$"))
        {
            throw new ArgumentException("Value must only be letters and white space.");
        }

        return new AlphaText(value);
    }
}