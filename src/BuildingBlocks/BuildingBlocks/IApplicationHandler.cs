namespace BuildingBlocks;

public interface IApplicationHandler<TRequest, TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}