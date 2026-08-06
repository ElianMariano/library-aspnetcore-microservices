using FastEndpoints;
using Loan.Application.Handlers.LoanRegistries.Commands.Return;

namespace Loan.Api.Endpoints.LoanRegistries.Commands;

public class ReturnLoanRegistryEndpoint : Endpoint<ReturnLoanRegistryCommand, ReturnLoanRegistryResult>
{
    private readonly ReturnLoanRegistryHandler _handler;

    public ReturnLoanRegistryEndpoint(ReturnLoanRegistryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/returnloanregistry");

        Description(x =>
        {
            x.WithTags("LoanRegistry");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        ReturnLoanRegistryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}