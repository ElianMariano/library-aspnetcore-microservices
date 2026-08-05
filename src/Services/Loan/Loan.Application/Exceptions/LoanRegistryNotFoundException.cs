namespace Loan.Application.Exceptions;

public sealed class LoanRegistryNotFoundException : ApplicationException
{
    public LoanRegistryNotFoundException(Guid loanRegistryId) : base("Loan Registry not found", loanRegistryId)
    {
    }
}