namespace JewelCollector;

/// <summary>
/// Representa uma joia que pode ser coletada pelo robô.
/// </summary>
internal class Jewel : Placeable
{
    #region Enums
    /// <summary>
    /// Tipo da joia.
    /// </summary>
    internal enum Types
    {
        Red,   /// Vermelho
        Green, /// Verde
        Blue   /// Azul
    }
    #endregion

    #region Fields
    private readonly Types _type;
    #endregion Fields

    #region Properties
    internal uint Value
    {
        get => _type switch
            {
                Types.Red   => 100,
                Types.Green => 50,
                Types.Blue  => 10,
                _           => 0
            };
    }
    #endregion Properties

    #region Constructor
    /// <summary>
    /// Construtor padrão da classe.
    /// </summary>
    /// <param name="position"> Posição no mapa. </param>
    /// <param name="type"> Tipo de joia. </param>
    internal Jewel(Tuple<byte, byte> position, Types type) : base(position)
    {
        _type = type;
    }
    #endregion Constructor
}
