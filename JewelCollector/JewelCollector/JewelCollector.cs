using Microsoft.Extensions.Logging;

namespace JewelCollector;

/// <summary>
/// Responsável por implementar o método Main(), criar o mapa, inserir as joias, obstáculos,
/// instanciar o robô e ler os comandos do teclado.
/// </summary>
public class JewelCollector
{
    #region Enums
    internal enum Inputs
    {
        MoveNorth,
        MoveWest,
        MoveSouth,
        MoveEast,
        Collect,
        Quit
    }
    #endregion Enums

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
        ushort currentLevel = 1;
        Map map;
        Random random = new();
        Robot player;

        try
        {
            // Inicializar objetos
            map = new Map(s_loggerFactory.CreateLogger<Map>(), 10);
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
                LogHelper.ConsumeLogs();
                Thread.Sleep(100); // garante que os logs serão escritos
                map.Print();
                player.PrintBag();

                // Ler o comando inserido pelo usuário
                Console.Write("Enter the command: ");
                string? command = Console.ReadLine();

                // Escolher o que fazer com o comando inserido
                switch (command)
                {
                    // Deslocar para o norte
                    case "w":
                        player.Energy--;
                        if (player.Energy > 0)
                            map.Move(player, IMoveable.MoveDirections.North);
                        else
                            LogHelper.QueueLog(s_logger, LogEntry.Types.Warning, "Not enough energy...");
                        break;

                    // Deslocar para o oeste
                    case "a":
                        player.Energy--;
                        if (player.Energy > 0)
                            map.Move(player, IMoveable.MoveDirections.West);
                        else
                            LogHelper.QueueLog(s_logger, LogEntry.Types.Warning, "Not enough energy...");
                        break;

                    // Deslocar para o sul
                    case "s":
                        player.Energy--;
                        if (player.Energy > 0)
                            map.Move(player, IMoveable.MoveDirections.South);
                        else
                            LogHelper.QueueLog(s_logger, LogEntry.Types.Warning, "Not enough energy...");
                        break;

                    // Deslocar para o leste
                    case "d":
                        player.Energy--;
                        if (player.Energy > 0)
                            map.Move(player, IMoveable.MoveDirections.East);
                        else
                            LogHelper.QueueLog(s_logger, LogEntry.Types.Warning, "Not enough energy...");
                        break;

                    // Pegar uma joia
                    case "g":
                        Placeable? item = map.GetAdjacentItem(player.Position);
                        if (item != null)
                            player.CollectItem(item);
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

                // Verificar se está sem energia
                if (player.Energy <= 0)
                {
                    isRunning = false;
                    s_logger.LogInformation("-- Game Over --");
                    Thread.Sleep(5000);
                    break;
                }

                // Verificar se a fase acabou
                if (map.CountJewels() <= 0)
                {
                    // Verificar se o jogador zerou o jogo
                    currentLevel++;
                    if (currentLevel > 31)
                    {
                        isRunning = false;
                        s_logger.LogInformation("Você zerou o jogo, parabéns!");
                        Thread.Sleep(5000);
                        break;
                    }

                    // Recriar o mapa
                    map = new Map(s_loggerFactory.CreateLogger<Map>(), (byte)(10 + currentLevel));

                    // Adicionar o jogador
                    Position position = map.GetEmptyPosition();
                    player = new Robot(s_loggerFactory.CreateLogger<Robot>(), position.y, position.x);
                    map.Place(player);

                    // Adicionar os items
                    for (int i = 0; i < 16 + currentLevel; i++)
                    {
                        // Obter posição vazia
                        position = map.GetEmptyPosition();

                        // Adicionar item
                        switch (random.Next(0, 2))
                        {
                            case 0: // Joia
                                switch (random.Next(0, 3))
                                {
                                    case 0: // Vermelho
                                        map.Place(new Jewel(position.y, position.x, Jewel.Types.Red));
                                        break;
                                    case 1: // Verde
                                        map.Place(new Jewel(position.y, position.x, Jewel.Types.Green));
                                        break;
                                    case 2: // Azul
                                        map.Place(new Jewel(position.y, position.x, Jewel.Types.Blue));
                                        break;
                                }
                                break;

                            case 1: // Obstáculo
                                switch (random.Next(0, 3))
                                {
                                    case 0: // Árvore
                                        map.Place(new Obstacle(position.y, position.x, Obstacle.Types.Tree));
                                        break;
                                    case 1: // Água
                                        map.Place(new Obstacle(position.y, position.x, Obstacle.Types.Water));
                                        break;
                                    case 2: // Radioativo
                                        map.Place(new Obstacle(position.y, position.x, Obstacle.Types.Radioactive));
                                        break;
                                }
                                break;
                        }
                    }
                }

            } while (isRunning);
        }
        catch (Exception ex)
        {
            LogHelper.QueueLog(s_logger, LogEntry.Types.Error, "Unhandled exception", ex);
        }
    }
}
