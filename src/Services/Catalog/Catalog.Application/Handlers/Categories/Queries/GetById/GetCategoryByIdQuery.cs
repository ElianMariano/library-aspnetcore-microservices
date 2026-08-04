using BuildingBlocks.DataTransferObjects;
using Catalog.Application.Dtos;

namespace Catalog.Application.Handlers.Categories.Queries.GetById;

public record GetCategoryByIdQuery(Guid categoryId);

public sealed class GetCategoryByIdResult(CategoryDto Data, int StatusCode = 200) : ResponseBase<CategoryDto>(Data, StatusCode);