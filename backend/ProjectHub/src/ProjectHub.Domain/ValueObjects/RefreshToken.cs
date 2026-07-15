namespace ProjectHub.Domain.ValueObjects;

public sealed class RefreshToken : IEquatable<RefreshToken>
{
    public string Value { get; private set; }


    private RefreshToken()
    {
        Value = string.Empty;
    }


    private RefreshToken(string value)
    {
        Value = value;
    }


    public static RefreshToken Create(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException(
                "Refresh token cannot be empty.",
                nameof(token));
        }


        return new RefreshToken(token);
    }


    public bool Equals(RefreshToken? other)
    {
        if (other is null)
            return false;


        return Value == other.Value;
    }


    public override bool Equals(object? obj)
    {
        return Equals(obj as RefreshToken);
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