using FastEndpoints;
using Loan.Application.Handlers.LoanRegistries.Queries.GetById;

namespace Loan.Api.Endpoints.LoanRegistries.Queries;

public class GetLoanRegistryByIdEndpoint : Endpoint<GetLoanRegistryByIdQuery, GetLoanRegistryByIdResult>
{
    private readonly GetLoanRegistryByIdHandler _handler;

    public GetLoanRegistryByIdEndpoint(GetLoanRegistryByIdHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/loanregistry/{{loanRegistryId}}");

        Description(x =>
        {
            x.WithTags("LoanRegistry");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetLoanRegistryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}