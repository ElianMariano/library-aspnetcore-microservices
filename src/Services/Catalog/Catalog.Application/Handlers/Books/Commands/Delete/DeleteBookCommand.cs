using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Books.Commands.Delete;

public record DeleteBookCommand(Guid bookId);

public class DeleteBookResult(Guid? Id) : ResponseBase<Guid?>(Id);

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.bookId).BookId();
    }
}