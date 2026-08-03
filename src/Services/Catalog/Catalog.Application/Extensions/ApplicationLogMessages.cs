using Microsoft.Extensions.Logging;

namespace Catalog.Application.Extensions;

public static class ApplicationLogMessages
{
    public const string MESSAGE_CREATE = "{0} succesfull with ID: {1}";
    public const string MESSAGE_UPDATE = "{0} succesfull with ID: {1}";
    public const string MESSAGE_DELETE = "{0} succesfull with ID: {1}";

    public static string FormatMessage(this string message, params object[] values)
    {
        string formattedMessage = string.Format(message, values);
        return formattedMessage;
    }

    public static void LogCreateInformation<T>(this ILogger<T> logger, Guid id)
    {
        logger.LogInformation(MESSAGE_CREATE.FormatMessage(typeof(T), id));
    }

    public static void LogUpdateInformation<T>(this ILogger<T> logger, Guid id)
    {
        logger.LogInformation(MESSAGE_UPDATE.FormatMessage(typeof(T), id));
    }

    public static void LogDeleteInformation<T>(this ILogger<T> logger, Guid id)
    {
        logger.LogInformation(MESSAGE_DELETE.FormatMessage(typeof(T), id));
    }
}