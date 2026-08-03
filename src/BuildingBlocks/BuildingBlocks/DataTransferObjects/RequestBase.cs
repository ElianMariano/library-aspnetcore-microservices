namespace BuildingBlocks.DataTransferObjects;

public record RequestBase<T>(
    T Data,
    DateTime? RequestTime = null)
{
    public DateTime EffectiveRequestTime => RequestTime ?? DateTime.UtcNow;
}