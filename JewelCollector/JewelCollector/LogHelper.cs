using Microsoft.Extensions.Logging;

namespace JewelCollector;

#pragma warning disable CA2254

/// <summary>
/// Classe que armazena entradas de log numa fila para exibi-las todas de uma vez.
/// </summary>
public static class LogHelper
{
    #region Fields
    private static readonly Queue<LogEntry> _queue = new();
    #endregion Fields

    #region Visible Methods
    /// <summary>
    /// Adiciona uma entrada de log na fila.
    /// </summary>
    /// <param name="logger"> Objeto responsável por registrar mensagens. </param>
    /// <param name="logType"> Nível (ou tipo) do log. </param>
    /// <param name="message"> Mensagem do log. </param>
    /// <param name="exception"> Exceção associada. </param>
    public static void QueueLog(ILogger logger, LogEntry.Types logType, string message, Exception? exception = null)
    {
        _queue.Enqueue(new LogEntry(logger, logType, message, exception));
    }

    /// <summary>
    /// Escreve todos os logs pendentes.
    /// </summary>
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
