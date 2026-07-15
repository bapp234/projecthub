namespace ProjectHub.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; private set; }

    private Email()
    {
        Value = string.Empty;
    }

    private Email(string value)
    {
        Value = value;
    }


    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email));

        email = email.Trim().ToLowerInvariant();


        if (!email.Contains("@"))
            throw new ArgumentException(
                "Email format is invalid.",
                nameof(email));


        return new Email(email);
    }


    public bool Equals(Email? other)
    {
        if (other is null)
            return false;

        return Value == other.Value;
    }


    public override bool Equals(object? obj)
    {
        return Equals(obj as Email);
    }


    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }


    public override string ToString()
    {
        return Value;
    }


    public static implicit operator string(Email email)
        => email.Value;
}