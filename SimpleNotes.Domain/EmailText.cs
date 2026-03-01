using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace SimpleNotes.Domain;

public class EmailText
{
    public static readonly EmailText Null = new EmailText(string.Empty);
    
    public string Value { get; }

    private EmailText(string value)
    {
        Value = value;
    }

    public bool IsNull => string.IsNullOrEmpty(Value);

    public static EmailText Create(string? value)
    {
        if (string.IsNullOrEmpty(value)) return Null;

        if (!Regex.IsMatch(value, "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$"))
        {
            throw new ArgumentException("Value must be in valid email format.");
        }

        return new EmailText(value);
    }
}
