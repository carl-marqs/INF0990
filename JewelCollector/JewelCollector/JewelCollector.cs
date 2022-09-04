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
        Map map;
        Robot player;

        try
        {
            // Inicializar objetos
            map = new Map(s_loggerFactory.CreateLogger<Map>(), 10, 10);
            map.Place(new Jewel(1, 9, Jewel.Types.Red));
            map.Place(new Jewel(8, 8, Jewel.Types.Red));
            map.Place(new Jewel(9, 1, Jewel.Types.Green));
            map.Place(new Jewel(7, 6, Jewel.Types.Green));
            map.Place(new Jewel(3, 4, Jewel.Types.Blue));
            map.Place(new Jewel(2, 1, Jewel.Types.Blue));
            map.Place(new Obstacle(5, 0, Obstacle.Types.Water));
            map.Place(new Obstacle(5, 1, Obstacle.Types.Water));
            map.Place(new Obstacle(5, 2, Obstacle.Types.Water));
            map.Place(new Obstacle(5, 3, Obstacle.Types.Water));
            map.Place(new Obstacle(5, 4, Obstacle.Types.Water));
            map.Place(new Obstacle(5, 5, Obstacle.Types.Water));
            map.Place(new Obstacle(5, 6, Obstacle.Types.Water));
            map.Place(new Obstacle(3, 9, Obstacle.Types.Tree));
            map.Place(new Obstacle(8, 3, Obstacle.Types.Tree));
            map.Place(new Obstacle(2, 5, Obstacle.Types.Tree));
            map.Place(new Obstacle(1, 4, Obstacle.Types.Tree));
            player = new Robot(s_loggerFactory.CreateLogger<Robot>(), 0, 0);
            map.Place(player);

            // Loop de execução
            do
            {
                // Exibir mapa, bolsa e logs
                Console.Clear();
                map.Print();
                player.PrintBag();
                LogHelper.ConsumeLogs();

                // Ler o comando inserido pelo usuário
                Console.Write("Enter the command: ");
                string? command = Console.ReadLine();

                // Escolher o que fazer com o comando inserido
                switch (command)
                {
                    // Deslocar para o norte
                    case "w":
                        map.Move(player, IMoveable.MoveDirections.North);
                        break;

                    // Deslocar para o oeste
                    case "a":
                        map.Move(player, IMoveable.MoveDirections.West);
                        break;

                    // Deslocar para o sul
                    case "s":
                        map.Move(player, IMoveable.MoveDirections.South);
                        break;

                    // Deslocar para o leste
                    case "d":
                        map.Move(player, IMoveable.MoveDirections.East);
                        break;

                    // Pegar uma joia
                    case "g":
                        Jewel? jewel = map.GetAdjacentJewel(player.Position);
                        if (jewel != null)
                            player.CollectJewel(jewel.Type);
                        break;

                    // Encerrar o jogo
                    case "quit":
                        isRunning = false;
                        break;

                    // Caso não seja nenhum dos definidos acima, o comando é inválido
                    default:
                        LogHelper.QueueLog(s_logger, LogEntry.Types.Warning, $"Unknown command: {command}", new InvalidOperationException());
                        break;
                }
            } while (isRunning);
        }
        catch (Exception ex)
        {
            LogHelper.QueueLog(s_logger, LogEntry.Types.Error, "Unhandled exception", ex);
        }
    }
}
