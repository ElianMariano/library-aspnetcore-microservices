using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Authors.Commands.Delete;

public record DeleteAuthorCommand(Guid authorId);

public class DeleteAuthorResult(Guid? Id) : ResponseBase<Guid?>(Id);

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.authorId).AuthorId();
    }
}