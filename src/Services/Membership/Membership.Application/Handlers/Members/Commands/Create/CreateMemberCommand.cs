using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Membership.Application.Dtos;
using Membership.Application.Rules;

namespace Membership.Application.Handlers.Members.Commands.Create;

public record CreateMemberCommand(MemberDto member);

public class CreateMemberResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberValidator()
    {
        RuleFor(x => x.member.name).MemberName();

        RuleFor(x => x.member.email).MemberEmail();
    }
}