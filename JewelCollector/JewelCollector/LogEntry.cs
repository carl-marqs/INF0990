using Microsoft.Extensions.Logging;

namespace JewelCollector;

/// <summary>
/// 
/// </summary>
public struct LogEntry
{
    #region Enums
    /// <summary>
    /// Tipos de log.
    /// </summary>
    public enum Types
    {
        Debug,
        Information,
        Warning,
        Error
    }
    #endregion Enums

    #region Fields
    internal readonly ILogger logger;
    internal readonly Types type;
    internal readonly string message;
    internal readonly Exception? exception;
    #endregion Fields

    #region Constructor
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="type"></param>
    /// <param name="message"></param>
    internal LogEntry(ILogger logger, Types type, string message, Exception? exception = null)
    {
        this.logger = logger;
        this.type = type;
        this.message = message;
        this.exception = exception;
    }
    #endregion Constructor
}