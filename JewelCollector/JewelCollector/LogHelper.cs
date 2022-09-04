using Microsoft.Extensions.Logging;

namespace JewelCollector;

#pragma warning disable CA2254

/// <summary>
/// 
/// </summary>
public static class LogHelper
{
    #region Fields
    private static readonly Queue<LogEntry> _queue = new();
    #endregion Fields

    #region Visible Methods
    public static void QueueLog(ILogger logger, LogEntry.Types logType, string message, Exception? exception = null)
    {
        _queue.Enqueue(new LogEntry(logger, logType, message, exception));
    }

    public static void ConsumeLogs()
    {
        while (_queue.Count > 0)
        {
            LogEntry entry = _queue.Dequeue();

            switch (entry.type)
            {
                case LogEntry.Types.Debug:
                    entry.logger.LogDebug(entry.message);
                    break;

                case LogEntry.Types.Information:
                    entry.logger.LogInformation(entry.message);
                    break;

                case LogEntry.Types.Warning:
                    if (entry.exception != null)
                        entry.logger.LogWarning(entry.exception, entry.message);
                    else
                        entry.logger.LogWarning(entry.message);
                    break;

                case LogEntry.Types.Error:
                    if (entry.exception != null)
                        entry.logger.LogError(entry.exception, entry.message);
                    else
                        entry.logger.LogError(entry.message);
                    break;
            }
        }
    }
    #endregion Visible Methods
}
