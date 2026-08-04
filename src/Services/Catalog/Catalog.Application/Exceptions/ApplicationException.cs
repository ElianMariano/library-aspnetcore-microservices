namespace Catalog.Application.Exceptions;

public abstract class ApplicationException : Exception
{
    public string ErrorCode { get; }

    public object[] Parameters { get; }

    protected ApplicationException(string errorCode, params object[] parameters)
    {
        ErrorCode = errorCode;
        Parameters = parameters;
    }
}