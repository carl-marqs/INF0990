namespace JewelCollector;

/// <summary>
/// Representa objetos que podem ser colocados no mapa.
/// </summary>
internal abstract class Placeable
{
    #region Properties
    internal Position Position { get; set; }
    internal abstract string Symbol { get; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição no mapa. </param>
    internal Placeable(byte y, byte x)
    {
        Position = new Position(y, x);
    }
    #endregion Constructor
}
