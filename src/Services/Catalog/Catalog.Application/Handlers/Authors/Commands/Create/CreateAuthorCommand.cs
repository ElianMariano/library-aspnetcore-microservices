using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Authors.Commands.Create;

public record CreateAuthorCommand(string name);

public class CreateAuthorResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.name).AuthorName();
    }
}