namespace P2Poker.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string? message) : base(message)
    {
    }

    public static void ThrowIfNull(object? o, string msg)
    {
        if (o is null) throw new NotFoundException(msg);
    }
}