using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Categories.Commands.Update;

public record UpdateCategoryCommand(CategoryDto category);

public class UpdateCategoryResult(Guid Id) : ResponseBase<Guid?>(Id);

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.category.categoryId).CategoryId();

        RuleFor(x => x.category.name).CategoryName();
    }
}