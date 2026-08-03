using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Books.Commands.Create;

public record CreateBookCommand(BookDto book);

public class CreateBookResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.book.title).BookTitle();

        RuleFor(x => x.book.isbn).BookISBN();

        RuleFor(x => x.book.authorId).AuthorId();

        RuleFor(x => x.book.categoryId).CategoryId();
    }
}