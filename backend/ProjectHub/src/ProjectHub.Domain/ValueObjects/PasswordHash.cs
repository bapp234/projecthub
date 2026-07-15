namespace ProjectHub.Domain.ValueObjects;

public sealed class PasswordHash : IEquatable<PasswordHash>
{
    public string Value { get; private set; }


    private PasswordHash()
    {
        Value = string.Empty;
    }


    private PasswordHash(string value)
    {
        Value = value;
    }


    public static PasswordHash Create(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
        {
            throw new ArgumentException(
                "Password hash cannot be empty.",
                nameof(hash));
        }


        return new PasswordHash(hash);
    }


    public bool Equals(PasswordHash? other)
    {
        if (other is null)
            return false;


        return Value == other.Value;
    }


    public override bool Equals(object? obj)
    {
        return Equals(obj as PasswordHash);
    }


    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }


    public override string ToString()
    {
        return Value;
    }
}