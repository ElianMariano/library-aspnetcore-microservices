namespace Membership.Infrastructure.Exceptions;

public abstract class InfrastructureException : Exception
{
    public string ErrorCode { get; }

    public object[] Parameters { get; }

    protected InfrastructureException(string errorCode, params object[] parameters)
    {
        ErrorCode = errorCode;
        Parameters = parameters;
    }
}