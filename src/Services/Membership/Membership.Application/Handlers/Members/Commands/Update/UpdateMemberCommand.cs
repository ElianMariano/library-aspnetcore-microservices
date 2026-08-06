using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Membership.Application.Dtos;
using Membership.Application.Rules;

namespace Membership.Application.Handlers.Members.Commands.Update;

public record UpdateMemberCommand(MemberDto member);

public class UpdateMemberResult(Guid Id) : ResponseBase<Guid?>(Id);

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(x => x.member.memberId).MemberId();

        RuleFor(x => x.member.name).MemberName();

        RuleFor(x => x.member.email).MemberEmail();
    }
}