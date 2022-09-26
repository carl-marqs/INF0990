namespace JewelCollector;

/// <summary>
/// Representa um obstáculo no mapa.
/// </summary>
internal class Obstacle : Placeable
{
    #region Enums
    /// <summary>
    /// Tipo do obstáculo.
    /// </summary>
    internal enum Types
    {
        Tree, /// Árvore
        Water, /// Água
        Radioactive // Radioativo
    }
    #endregion

    #region Properties
    /// <summary>
    /// Símbolo usado para representar o obstáculo no mapa.
    /// </summary>
    internal override string Symbol
    {
        get => Type switch
        {
            Types.Tree => "$$",
            Types.Water => "##",
            Types.Radioactive => "!!",

            _ => "??"
        };
    }
    /// <summary>
    /// Tipo do obstáculo.
    /// </summary>
    internal Types Type { get; }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="y"> Posição 'y' no mapa. </param>
    /// <param name="x"> Posição 'x' no mapa. </param>
    /// <param name="type"> Tipo do obstáculo. </param>
    /// <returns></returns>
    internal Obstacle(byte y, byte x, Types type) : base(y, x)
    {
        Type = type;
    }
    #endregion Constructor
}
