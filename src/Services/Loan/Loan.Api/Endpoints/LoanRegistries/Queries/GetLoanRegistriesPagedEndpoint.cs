using FastEndpoints;
using Loan.Application.Handlers.LoanRegistries.Queries.GetPaged;

namespace Loan.Api.Endpoints.LoanRegistries.Queries;

public class GetLoanRegistriesPagedEndpoint : Endpoint<GetLoanRegistriesPagedQuery, GetLoanRegistriesPagedResult>
{
    private readonly GetLoanRegistriesPagedHandler _handler;

    public GetLoanRegistriesPagedEndpoint(GetLoanRegistriesPagedHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Get($"/loanregistries");

        Description(x =>
        {
            x.WithTags("LoanRegistry");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetLoanRegistriesPagedQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}