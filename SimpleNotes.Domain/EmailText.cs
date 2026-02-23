using System.Text.RegularExpressions;

namespace SimpleNotes.Domain;

public class EmailText
{
    public static readonly EmailText Null = new EmailText(string.Empty);

    public string Value { get; }

    public EmailText(string value)
    {
        if (!Regex.IsMatch(value, "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$"))
        {
            throw new ArgumentException("Value must be in valid email format.");
        }

        Value = value;
    }

    public bool IsNull => this == Null;

    public static EmailText Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Null;
        }
        return new EmailText(value);
    }
}
