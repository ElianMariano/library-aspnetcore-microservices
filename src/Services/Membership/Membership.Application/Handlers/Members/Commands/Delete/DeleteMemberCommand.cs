using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Membership.Application.Rules;

namespace Membership.Application.Handlers.Members.Commands.Delete;

public record DeleteMemberCommand(Guid memberId);

public class DeleteMemberResult(Guid? Id) : ResponseBase<Guid?>(Id);

public class DeleteMemberValidator : AbstractValidator<DeleteMemberCommand>
{
    public DeleteMemberValidator()
    {
        RuleFor(x => x.memberId).MemberId();
    }
}