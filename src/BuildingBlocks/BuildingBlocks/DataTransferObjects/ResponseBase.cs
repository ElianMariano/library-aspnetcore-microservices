namespace BuildingBlocks.DataTransferObjects;

public abstract class ResponseBase<T>(
    T data,
    int statusCode = 200)
{
    public T? Data { get; set; } = data;

    public int StatusCode { get; set; } = statusCode;

    public bool Success => StatusCode is >= 200 and <= 299;
}