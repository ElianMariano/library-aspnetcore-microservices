using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Books.Commands.Update;

public record UpdateBookCommand(BookDto book);

public class UpdateBookResult(Guid Id) : ResponseBase<Guid?>(Id);

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.book.id).BookId();

        RuleFor(x => x.book.title).BookTitle();

        RuleFor(x => x.book.isbn).BookISBN();
    }
}