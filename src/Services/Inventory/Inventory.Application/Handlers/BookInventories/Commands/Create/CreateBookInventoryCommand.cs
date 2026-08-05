using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Inventory.Application.Dtos;
using Inventory.Application.Rules;

namespace Inventory.Application.Handlers.BookInventories.Commands.Create;

public record CreateBookInventoryCommand(BookInventoryDto bookInventory);

public class CreateBookInventoryResult(Guid Id) : ResponseBase<Guid?>(Id);

public class CreateBookInventoryValidator : AbstractValidator<CreateBookInventoryCommand>
{
    public CreateBookInventoryValidator()
    {
        RuleFor(x => x.bookInventory.bookId).BookId();
    }
}