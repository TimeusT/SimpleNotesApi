using FluentResults;

namespace SimpleNotes.Infrastructure;

public class KnownError : Error
{
    public KnownError(int statusCode)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}

public class ValidationError : KnownError
{
    public ValidationError() : base(400)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public IDictionary<string, string[]> Errors { get; }

    public ValidationError WithError(string key, string message)
    {
        if (Errors.ContainsKey(key))
        {
            Errors[key] = Errors[key].Append(message).ToArray();
        }
        else
        {
            Errors[key] = [message];
        }
        return this;
    }
}
