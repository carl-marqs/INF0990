namespace JewelCollector;

/// <summary>
/// Representa uma posição no mapa.
/// </summary>
internal struct Position
{
    #region Fields
    /// <summary>
    /// Posição 'x', representando as colunas do mapa.
    /// </summary>
    internal readonly byte x;
    /// <summary>
    /// Posição 'y', representando as linhas do mapa.
    /// </summary>
    internal readonly byte y;
    #endregion Fields

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="x"> Posição 'x' no mapa. </param>
    /// <param name="y"> Posição 'y' no mapa. </param>
    internal Position(byte y, byte x)
    {
        this.y = y;
        this.x = x;
    }
    #endregion Constructor

    #region Visible Methods
    /// <summary>
    /// Converte o objeto para string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"({x}, {y})";
    }
    #endregion Visible Methods
}