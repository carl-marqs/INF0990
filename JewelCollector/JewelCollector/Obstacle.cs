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
        Water /// Água
    }
    #endregion

    #region Fields
    private readonly Types _type;
    #endregion Fields

    #region Properties
    internal override string Symbol
    {
        get => _type switch
        {
            Types.Tree => "$$",
            Types.Water => "##",
            _ => "??"
        };
    }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição no mapa. </param>
    /// <param name="type"> Tipo de obstáculo. </param>
    internal Obstacle(byte y, byte x, Types type) : base(y, x)
    {
        _type = type;
    }
    #endregion Constructor
}
