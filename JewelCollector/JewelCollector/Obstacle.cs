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

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição no mapa. </param>
    /// <param name="type"> Tipo de obstáculo. </param>
    internal Obstacle(Tuple<byte, byte> position, Types type) : base(position)
    {
        _type = type;
    }
    #endregion Constructor
}
