using FastEndpoints;
using Loan.Application.Handlers.LoanRegistries.Commands.Create;

namespace Loan.Api.Endpoints.LoanRegistries.Commands;

public class CreateLoanRegistryEndpoint : Endpoint<CreateLoanRegistryCommand, CreateLoanRegistryResult>
{
    private readonly CreateLoanRegistryHandler _handler;

    public CreateLoanRegistryEndpoint(CreateLoanRegistryHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/loanregistry");

        Description(x =>
        {
            x.WithTags("LoanRegistry");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateLoanRegistryCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _handler.Handle(request, cancellationToken);
        await Send.OkAsync(response);
    }
}