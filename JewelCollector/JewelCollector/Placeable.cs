namespace JewelCollector;

/// <summary>
/// Representa objetos que podem ser colocados no mapa.
/// </summary>
internal abstract class Placeable
{
    #region Properties
    /// <summary>
    /// Posição do item no mapa.
    /// </summary>
    internal Position Position { get; set; }
    /// <summary>
    /// Símbolo usado para representar o item no mapa.
    /// </summary>
    internal abstract string Symbol { get; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="y"> Posição 'y' no mapa. </param>
    /// <param name="x"> Posição 'x' no mapa. </param>
    internal Placeable(byte y, byte x)
    {
        Position = new Position(y, x);
    }
    #endregion Constructor
}
