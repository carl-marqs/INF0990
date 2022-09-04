using System.Collections.ObjectModel;
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
    #endregion Fields

    #region Properties
    internal override string Symbol { get => "ME"; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição inicial no mapa. </param>
    internal Robot(ILogger<Robot> logger, byte y, byte x) : base(y, x)
    {
        _logger = logger;
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// 
    /// </summary>
    internal void CollectJewel(Jewel.Types type)
    {
        _bag[(int)type] += 1;
        _logger.LogInformation("Jewel collected");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Position newPosition)
    {
        Position = newPosition;
        _logger.LogDebug("Moved to {Position}", Position);
    }

    /// <summary>
    /// 
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

        Console.WriteLine($"Bag total items: {totalItems} | Bag total value: {totalValue}");
    }
    #endregion Visible Methods
}
