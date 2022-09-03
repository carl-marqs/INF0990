using Microsoft.Extensions.Logging;

namespace JewelCollector;

/// <summary>
/// Responsável por implementar o método Main(), criar o mapa, inserir as joias, obstáculos,
/// instanciar o robô e ler os comandos do teclado.
/// </summary>
public class JewelCollector
{
    #region Fields
    private static readonly ILogger<JewelCollector> s_logger;
    private static readonly ILoggerFactory s_loggerFactory;
    #endregion Fields

    #region Constructor
    /// <summary>
    /// Construtor estático responsável por criar o logger factory e
    /// o log da própria classe.
    /// </summary>
    static JewelCollector()
    {
        // Inicializar o logger factory que será usado para criar loggers
        s_loggerFactory = LoggerFactory.Create(builder =>
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss ";
            }));
        // Criar novo logger e associá-lo à classe atual
        s_logger = s_loggerFactory.CreateLogger<JewelCollector>();
    }
    #endregion Constructor

    /// <summary>
    /// Ponto de entrada do programa, responsável pelo loop de execução.
    /// </summary>
    public static void Main()
    {
        bool isRunning = true;

        try
        {
            // Loop de execução
            do
            {
                // Ler o comando inserido pelo usuário
                Console.WriteLine("Enter the command: ");
                string? command = Console.ReadLine();

                // Checar se o comando é válido
                if (string.IsNullOrWhiteSpace(command))
                    throw new InvalidOperationException("Null or whitespace command.");
                
                // Escolher o que fazer com o comando inserido
                switch (command)
                {
                    // Descolar para o norte
                    case "w":
                        break;

                    // Descolcar para o oeste
                    case "a":
                        break;

                    // Deslocar para o sul
                    case "s":
                        break;

                    // Deslocar para o leste
                    case "d":
                        break;

                    // Pegar uma joia
                    case "g":
                        break;

                    // Encerrar o jogo
                    case "quit":
                        isRunning = false;
                        break;

                    // Caso não seja nenhum dos definidos acima, o comando é inválido
                    default:
                        throw new InvalidOperationException($"Unknown command: {command}");
                }
            } while (isRunning);
        }
        catch (Exception ex)
        {
            s_logger.LogCritical(ex, "Unhandled exception");
        }
    }
}
