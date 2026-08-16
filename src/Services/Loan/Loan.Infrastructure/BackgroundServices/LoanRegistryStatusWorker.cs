using BuildingBlocks.Messaging.Events;
using Loan.Application.Data;
using Loan.Domain.Enumerables;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Loan.Infrastructure.BackgroundServices;

public class LoanRegistryStatusWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<LoanRegistryStatusWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(6));
        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            try
            {
                await CheckExpiredReservations(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking expired reservations");
            }
        }
    }

    private async Task CheckExpiredReservations(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        var loanRegistries = await context.LoanRegistries.Where(x => x.DueDate <= DateOnly.FromDateTime(DateTime.UtcNow) && x.Status == LoanRegistryStatus.Borrowed).ToListAsync();
        foreach (var loanRegistry in loanRegistries)
        {
            loanRegistry.ChangeStatus(LoanRegistryStatus.Overdue);
            await PublishOverdueLoan(loanRegistry.UserId, cancellationToken);
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishOverdueLoan(Guid userId, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        var memberHasOverdueLoanEvent = new MemberHasOverdueLoanEvent(userId);
        await publishEndpoint.Publish(memberHasOverdueLoanEvent, cancellationToken);
    }
}