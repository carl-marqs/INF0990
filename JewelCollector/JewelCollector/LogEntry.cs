using Microsoft.Extensions.Logging;

namespace JewelCollector;

/// <summary>
/// Representa uma entrada de log.
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
    /// <summary>
    /// Objeto responsável por registrar mensagens.
    /// </summary>
    internal readonly ILogger logger;
    /// <summary>
    /// Nível (ou tipo) do log.
    /// </summary>
    internal readonly Types type;
    /// <summary>
    /// Mensagem do log.
    /// </summary>
    internal readonly string message;
    /// <summary>
    /// Exceção associada.
    /// </summary>
    internal readonly Exception? exception;
    #endregion Fields

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="logger"> Objeto responsável por registrar mensagens. </param>
    /// <param name="type"> Nível (ou tipo) do log. </param>
    /// <param name="message"> Mensagem do log. </param>
    /// <param name="exception"> Exceção associada. </param>
    internal LogEntry(ILogger logger, Types type, string message, Exception? exception = null)
    {
        this.logger = logger;
        this.type = type;
        this.message = message;
        this.exception = exception;
    }
    #endregion Constructor
}