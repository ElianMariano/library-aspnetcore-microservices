using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Categories.Commands.Delete;

public record DeleteCategoryCommand(Guid categoryId);

public class DeleteCategoryResult(Guid? Id) : ResponseBase<Guid?>(Id);

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.categoryId).CategoryId();
    }
}