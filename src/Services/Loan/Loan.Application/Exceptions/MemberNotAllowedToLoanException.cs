using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.Application.Exceptions;

public sealed class MemberNotAllowedToLoanException : ApplicationException
{
    public MemberNotAllowedToLoanException(Guid memberId) : base("Member not allowed to loan ", memberId)
    {
    }
}