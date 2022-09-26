using Microsoft.Extensions.Logging;

namespace JewelCollector;

/// <summary>
/// Representa o personagem controlado pelo jogador.
/// </summary>
internal class Robot : Placeable, IMoveable
{
    #region Fields
    private readonly ILogger<Robot> _logger;
    private readonly ushort[] _bag = new ushort[Enum.GetNames(typeof(Jewel.Types)).Length];
    private ushort _collectedTrees = 0;
    #endregion Fields

    #region Properties
    /// <summary>
    /// Símbolo usado para representar o jogador.
    /// </summary>
    internal override string Symbol { get => "ME"; }
    /// <summary>
    /// Quantidade de energia que o jogador usa para se mover.
    /// </summary>
    internal ushort Energy { get; set; } = 20;
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="logger"> Objeto responsável por registrar mensagens. </param>
    /// <param name="y"> Posição 'y' no mapa. </param>
    /// <param name="x"> Posição 'x' no mapa. </param>
    /// <returns></returns>
    internal Robot(ILogger<Robot> logger, byte y, byte x) : base(y, x)
    {
        _logger = logger;
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// Coleta um item, usa seu efeito e adiciona na sacola.
    /// </summary>
    internal void CollectItem(Placeable item)
    {
        if (item is Jewel jewel)
        {
            // Adicionar na sacola
            _bag[(int)jewel.Type] += 1;

            // Usar a joia
            if (jewel.Type == Jewel.Types.Blue)
                Energy += 5;
        }
        else if (item is Obstacle obstacle && obstacle.Type == Obstacle.Types.Tree)
        {
            // Adicionar na sacola
            _collectedTrees += 1;

            // Usar o obstáculo
            Energy += 1;
        }

        LogHelper.QueueLog(_logger, LogEntry.Types.Information, "Item collected");
    }

    /// <summary>
    /// Move-se para determinada posição.
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Position newPosition)
    {
        Position = newPosition;
        LogHelper.QueueLog(_logger, LogEntry.Types.Debug, $"Moved to {Position}");
    }

    /// <summary>
    /// Exibe informações da sacola.
    /// </summary>
    internal void PrintBag()
    {
        uint totalItems = 0;
        ushort totalValue = 0;

        foreach (Jewel.Types type in Enum.GetValues(typeof(Jewel.Types)).Cast<Jewel.Types>())
        {
            totalItems += _bag[(int)type];
            totalValue += (ushort)(_bag[(int)type] * Jewel.GetValue(type));
        }
        totalItems += _collectedTrees;

        Console.WriteLine($"Bag total items: {totalItems} | Bag total value: {totalValue} | Energy: {Energy}");
    }
    #endregion Visible Methods
}
