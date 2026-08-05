using BuildingBlocks.DataTransferObjects;
using FluentValidation;
using Inventory.Application.Rules;

namespace Inventory.Application.Handlers.BookInventories.Commands.Delete;

public record DeleteBookInventoryCommand(Guid bookInventoryId);

public class DeleteBookInventoryResult(Guid? Id) : ResponseBase<Guid?>(Id);

public class DeleteBookInventoryValidator : AbstractValidator<DeleteBookInventoryCommand>
{
    public DeleteBookInventoryValidator()
    {
        RuleFor(x => x.bookInventoryId).BookInventoryId();
    }
}