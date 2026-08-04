using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;
using Catalog.Application.Rules;
using FluentValidation;

namespace Catalog.Application.Handlers.Authors.Commands.Update;

public record UpdateAuthorCommand(AuthorDto author);

public class UpdateAuthorResult(Guid Id) : ResponseBase<Guid?>(Id);

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.author.authorId).AuthorId();

        RuleFor(x => x.author.name).AuthorName();
    }
}