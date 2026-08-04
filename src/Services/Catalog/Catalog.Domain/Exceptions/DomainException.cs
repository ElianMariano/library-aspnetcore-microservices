namespace Catalog.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public string ErrorCode { get; }

    public object[] Parameters { get; }

    protected DomainException(string errorCode, params object[] parameters)
    {
        ErrorCode = errorCode;
        Parameters = parameters;
    }
}