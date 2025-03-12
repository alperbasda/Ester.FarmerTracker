namespace Ester.FarmetTracker.Common.Exceptions;

public class ErrorMessageNotFoundException : Exception
{
    public ErrorMessageNotFoundException(string message) : base(message)
    {
    }
}
