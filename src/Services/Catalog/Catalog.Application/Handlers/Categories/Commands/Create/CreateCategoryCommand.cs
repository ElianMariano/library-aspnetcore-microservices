using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Categories.Commands.Create;

public record CreateCategoryCommand(string name);

public class CreateCategoryResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.name).CategoryName();
    }
}