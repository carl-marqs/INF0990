namespace JewelCollector;

/// <summary>
/// Representa objetos que podem ser colocados no mapa.
/// </summary>
abstract class Placeable
{
    #region Fields
    internal readonly Tuple<byte, byte> _position;
    #endregion Fields

    #region Properties
    internal abstract string Symbol { get; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição no mapa. </param>
    internal Placeable(Tuple<byte, byte> position)
    {
        _position = position;
    }
    #endregion Constructor
}
